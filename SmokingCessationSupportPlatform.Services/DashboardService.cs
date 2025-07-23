using SmokingCessationSupportPlatform.DataAccessObjects.Contexts;

namespace SmokingCessationSupportPlatform.Services
{
    public class DashboardService
    {
        private readonly SmokingCessationSupportPlatformContext _context;

        public DashboardService(SmokingCessationSupportPlatformContext context)
        {
            _context = context;
        }

        public int TotalActivePlans() =>
            _context.MembershipPlans.Count(p => p.IsActive == true);

        public int TotalInactivePlans() =>
            _context.MembershipPlans.Count(p => p.IsActive == false);

        public int TotalUserMemberships() =>
            _context.UserMemberships.Count();

        public decimal TotalRevenue() =>
            _context.UserMemberships
                .Where(um => um.PaymentStatus == "Paid")
                .Sum(um => um.Plan.Price);

        public Dictionary<string, int> UsersPerPlan() =>
            _context.UserMemberships
                .GroupBy(um => um.Plan.PlanName)
                .ToDictionary(g => g.Key, g => g.Count());
    }
}
