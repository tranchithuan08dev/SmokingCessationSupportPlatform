using SmokingCessationSupportPlatform.BusinessObjects.Models;

namespace SmokingCessationSupportPlatform.Services;

public interface IMembershipService
{
    Task<IEnumerable<MembershipPlan>> GetAllActivePlansAsync();
    Task<MembershipPlan?> GetPlanByIdAsync(int planId);
    Task<UserMembership?> GetUserMembershipAsync(int userId);
    Task<UserMembership> SubscribeToPlanAsync(int userId, int planId);
    Task<bool> ProcessPaymentAsync(int userMembershipId, string paymentMethod, string transactionId);
    Task<bool> IsUserActiveMemberAsync(int userId);
    Task<DateTime?> GetUserMembershipEndDateAsync(int userId);
    Task<bool> CancelMembershipAsync(int userId);
} 