using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmokingCessationSupportPlatform.Models;
using SmokingCessationSupportPlatform.Services;
using SmokingCessationSupportPlatform.BusinessObjects.Models;
using System.Security.Claims;

namespace SmokingCessationSupportPlatform.Controllers;

[Authorize]
public class SmokingStatusController : Controller
{
    private readonly ISmokingStatusService _service;
    public SmokingStatusController(ISmokingStatusService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var latest = await _service.GetLatestStatusByUserIdAsync(userId);
        var history = await _service.GetAllStatusByUserIdAsync(userId);
        var displayList = history.Select(s => new SmokingStatusDisplayViewModel
        {
            StatusId = s.StatusId,
            ReportDate = s.ReportDate,
            CigarettesPerDay = s.CigarettesPerDay,
            Frequency = s.Frequency,
            CigaretteCostPerPack = s.CigaretteCostPerPack,
            PacksPerWeek = s.PacksPerWeek
        }).ToList();
        ViewBag.Latest = latest != null ? new SmokingStatusDisplayViewModel
        {
            StatusId = latest.StatusId,
            ReportDate = latest.ReportDate,
            CigarettesPerDay = latest.CigarettesPerDay,
            Frequency = latest.Frequency,
            CigaretteCostPerPack = latest.CigaretteCostPerPack,
            PacksPerWeek = latest.PacksPerWeek
        } : null;
        return View(displayList);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new SmokingStatusInputViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SmokingStatusInputViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var status = new SmokingStatus
        {
            UserId = userId,
            CigarettesPerDay = model.CigarettesPerDay,
            Frequency = model.Frequency,
            CigaretteCostPerPack = model.CigaretteCostPerPack,
            PacksPerWeek = model.PacksPerWeek,
            ReportDate = model.ReportDate ?? DateOnly.FromDateTime(DateTime.Now)
        };
        await _service.AddStatusAsync(status);
        TempData["SuccessMessage"] = "Ghi nhận tình trạng thành công!";
        return RedirectToAction("Index");
    }
} 