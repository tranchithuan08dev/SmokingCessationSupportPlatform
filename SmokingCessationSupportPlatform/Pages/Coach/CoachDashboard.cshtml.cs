using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SmokingCessationSupportPlatform.Web.Pages.Coach
{
    public class CoachDashboardModel : PageModel
    {
        public IActionResult OnGet()
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            if (role != "Coach")
            {
                return RedirectToPage("Account/AccessDenied");
            }
            return Page();
        }
    }
}
