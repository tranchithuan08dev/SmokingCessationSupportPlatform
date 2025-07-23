using SmokingCessationSupportPlatform.BusinessObjects.Models;

namespace SmokingCessationSupportPlatform.Repositories;

public interface IMembershipRepository
{
    Task<IEnumerable<MembershipPlan>> GetAllActivePlansAsync();
    Task<MembershipPlan?> GetPlanByIdAsync(int planId);
    Task<UserMembership?> GetUserMembershipAsync(int userId);
    Task<IEnumerable<UserMembership>> GetAllUserMembershipsAsync();
    Task<UserMembership> CreateUserMembershipAsync(UserMembership userMembership);
    Task<UserMembership> UpdateUserMembershipAsync(UserMembership userMembership);
    Task<bool> IsUserActiveMemberAsync(int userId);
    Task<DateTime?> GetUserMembershipEndDateAsync(int userId);
} 