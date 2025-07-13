using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.Repositories;

namespace SmokingCessationSupportPlatform.Services;

public class MembershipService : IMembershipService
{
    private readonly IMembershipRepository _membershipRepository;

    public MembershipService(IMembershipRepository membershipRepository)
    {
        _membershipRepository = membershipRepository;
    }

    public async Task<IEnumerable<MembershipPlan>> GetAllActivePlansAsync()
    {
        return await _membershipRepository.GetAllActivePlansAsync();
    }

    public async Task<MembershipPlan?> GetPlanByIdAsync(int planId)
    {
        return await _membershipRepository.GetPlanByIdAsync(planId);
    }

    public async Task<UserMembership?> GetUserMembershipAsync(int userId)
    {
        return await _membershipRepository.GetUserMembershipAsync(userId);
    }

    public async Task<UserMembership> SubscribeToPlanAsync(int userId, int planId)
    {
        var plan = await _membershipRepository.GetPlanByIdAsync(planId);
        if (plan == null)
        {
            throw new ArgumentException("Invalid plan ID");
        }

        var userMembership = new UserMembership
        {
            UserId = userId,
            PlanId = planId,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(plan.DurationDays ?? 30),
            PaymentStatus = "Pending"
        };

        return await _membershipRepository.CreateUserMembershipAsync(userMembership);
    }

    public async Task<bool> ProcessPaymentAsync(int userMembershipId, string paymentMethod, string transactionId)
    {
        // In a real application, you would integrate with a payment gateway here
        // For now, we'll simulate a successful payment
        
        // Get all user memberships and find the one with the specified ID
        var allMemberships = await _membershipRepository.GetAllUserMembershipsAsync();
        var userMembership = allMemberships.FirstOrDefault(um => um.UserMembershipId == userMembershipId);
        
        if (userMembership == null)
        {
            return false;
        }

        userMembership.PaymentStatus = "Paid";
        await _membershipRepository.UpdateUserMembershipAsync(userMembership);
        
        return true;
    }

    public async Task<bool> IsUserActiveMemberAsync(int userId)
    {
        return await _membershipRepository.IsUserActiveMemberAsync(userId);
    }

    public async Task<DateTime?> GetUserMembershipEndDateAsync(int userId)
    {
        return await _membershipRepository.GetUserMembershipEndDateAsync(userId);
    }

    public async Task<bool> CancelMembershipAsync(int userId)
    {
        var userMembership = await _membershipRepository.GetUserMembershipAsync(userId);
        if (userMembership == null)
        {
            return false;
        }

        userMembership.EndDate = DateTime.Now;
        await _membershipRepository.UpdateUserMembershipAsync(userMembership);
        
        return true;
    }
} 