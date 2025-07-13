using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmokingCessationSupportPlatform.Models;
using SmokingCessationSupportPlatform.Services;
using System.Security.Claims;

namespace SmokingCessationSupportPlatform.Controllers;

[Authorize]
public class MembershipController : Controller
{
    private readonly IMembershipService _membershipService;

    public MembershipController(IMembershipService membershipService)
    {
        _membershipService = membershipService;
    }

    public async Task<IActionResult> Index()
    {
        var plans = await _membershipService.GetAllActivePlansAsync();
        var planViewModels = plans.Select(p => new MembershipPlanViewModel
        {
            PlanId = p.PlanId,
            PlanName = p.PlanName,
            Description = p.Description ?? "",
            Price = p.Price,
            DurationDays = p.DurationDays ?? 30
        }).ToList();

        return View(planViewModels);
    }

    public async Task<IActionResult> Subscribe(int planId)
    {
        var plan = await _membershipService.GetPlanByIdAsync(planId);
        if (plan == null)
        {
            return NotFound();
        }

        var paymentViewModel = new PaymentViewModel
        {
            PlanId = planId,
            Amount = plan.Price
        };

        ViewBag.Plan = plan;
        return View(paymentViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Subscribe(PaymentViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var plan = await _membershipService.GetPlanByIdAsync(model.PlanId);
            ViewBag.Plan = plan;
            return View(model);
        }

        try
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            
            // Subscribe to plan
            var userMembership = await _membershipService.SubscribeToPlanAsync(userId, model.PlanId);
            
            // Process payment (simulated)
            var paymentSuccess = await _membershipService.ProcessPaymentAsync(
                userMembership.UserMembershipId, 
                model.PaymentMethod, 
                Guid.NewGuid().ToString());

            if (paymentSuccess)
            {
                TempData["SuccessMessage"] = "Đăng ký thành viên thành công!";
                return RedirectToAction(nameof(Status));
            }
            else
            {
                ModelState.AddModelError("", "Thanh toán thất bại. Vui lòng thử lại.");
                var plan = await _membershipService.GetPlanByIdAsync(model.PlanId);
                ViewBag.Plan = plan;
                return View(model);
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Có lỗi xảy ra: " + ex.Message);
            var plan = await _membershipService.GetPlanByIdAsync(model.PlanId);
            ViewBag.Plan = plan;
            return View(model);
        }
    }

    public async Task<IActionResult> Status()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var userMembership = await _membershipService.GetUserMembershipAsync(userId);
        var isActiveMember = await _membershipService.IsUserActiveMemberAsync(userId);

        var statusViewModel = new MembershipStatusViewModel
        {
            IsActiveMember = isActiveMember,
            PlanName = userMembership?.Plan?.PlanName,
            StartDate = userMembership?.StartDate,
            EndDate = userMembership?.EndDate,
            PaymentStatus = userMembership?.PaymentStatus,
            DaysRemaining = userMembership?.EndDate > DateTime.Now 
                ? (userMembership.EndDate.Value - DateTime.Now).Days 
                : 0
        };

        return View(statusViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var success = await _membershipService.CancelMembershipAsync(userId);

        if (success)
        {
            TempData["SuccessMessage"] = "Đã hủy thành viên thành công!";
        }
        else
        {
            TempData["ErrorMessage"] = "Không thể hủy thành viên. Vui lòng thử lại.";
        }

        return RedirectToAction(nameof(Status));
    }
} 