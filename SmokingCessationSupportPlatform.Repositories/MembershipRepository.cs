using Microsoft.EntityFrameworkCore;
using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.DataAccessObjects.Contexts;

namespace SmokingCessationSupportPlatform.Repositories;

public class MembershipRepository : IMembershipRepository
{
    private readonly SmokingCessationSupportPlatformContext _context;

    public MembershipRepository(SmokingCessationSupportPlatformContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MembershipPlan>> GetAllActivePlansAsync()
    {
        return await _context.MembershipPlans
            .Where(p => p.IsActive == true)
            .OrderBy(p => p.Price)
            .ToListAsync();
    }

    public async Task<MembershipPlan?> GetPlanByIdAsync(int planId)
    {
        return await _context.MembershipPlans
            .FirstOrDefaultAsync(p => p.PlanId == planId && p.IsActive == true);
    }

    public async Task<UserMembership?> GetUserMembershipAsync(int userId)
    {
        return await _context.UserMemberships
            .Include(um => um.Plan)
            .Where(um => um.UserId == userId)
            .OrderByDescending(um => um.StartDate)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<UserMembership>> GetAllUserMembershipsAsync()
    {
        return await _context.UserMemberships
            .Include(um => um.Plan)
            .Include(um => um.User)
            .ToListAsync();
    }

    public async Task<UserMembership> CreateUserMembershipAsync(UserMembership userMembership)
    {
        _context.UserMemberships.Add(userMembership);
        await _context.SaveChangesAsync();
        return userMembership;
    }

    public async Task<UserMembership> UpdateUserMembershipAsync(UserMembership userMembership)
    {
        _context.UserMemberships.Update(userMembership);
        await _context.SaveChangesAsync();
        return userMembership;
    }

    public async Task<bool> IsUserActiveMemberAsync(int userId)
    {
        var membership = await _context.UserMemberships
            .Where(um => um.UserId == userId)
            .OrderByDescending(um => um.StartDate)
            .FirstOrDefaultAsync();

        if (membership == null) return false;

        return membership.EndDate > DateTime.Now && 
               membership.PaymentStatus == "Paid";
    }

    public async Task<DateTime?> GetUserMembershipEndDateAsync(int userId)
    {
        var membership = await _context.UserMemberships
            .Where(um => um.UserId == userId)
            .OrderByDescending(um => um.StartDate)
            .FirstOrDefaultAsync();

        return membership?.EndDate;
    }
} 