using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokingCessationSupportPlatform.Services;

namespace SmokingCessationSupportPlatform.Web.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class DashboardModel : PageModel
    {
        private readonly DashboardService _dashboardService;

        public DashboardModel(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public int ActivePlans { get; set; }
        public int InactivePlans { get; set; }
        public int TotalUsers { get; set; }
        public decimal Revenue { get; set; }
        public Dictionary<string, int> UsersPerPlan { get; set; }

        public void OnGet()
        {
            ActivePlans = _dashboardService.TotalActivePlans();
            InactivePlans = _dashboardService.TotalInactivePlans();
            TotalUsers = _dashboardService.TotalUserMemberships();
            Revenue = _dashboardService.TotalRevenue();
            UsersPerPlan = _dashboardService.UsersPerPlan();
        }
    }
}