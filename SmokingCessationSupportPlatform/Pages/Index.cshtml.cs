using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SmokingCessationSupportPlatform.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult  OnGet()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var userRoleClaim = User.FindFirst(ClaimTypes.Role);

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int currentId))
            {
                _logger.LogWarning("OnGetAsync: User ID claim not found or invalid. Redirecting to login.");
                return RedirectToPage("/Account/Login");
            }
            var CurrentUserType = userRoleClaim;
            if (CurrentUserType == null)
            {
                _logger.LogWarning("OnGetAsync: User role claim not found. Redirecting to login.");
                return RedirectToPage("/Account/Login");
            }

            if (CurrentUserType.Value.Equals("Coach", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToPage("/Coach/CoachDashboard");

            }
            return Page();


        }
    }
}
