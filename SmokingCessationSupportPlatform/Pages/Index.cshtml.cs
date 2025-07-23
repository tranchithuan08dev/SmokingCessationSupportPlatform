using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokingCessationSupportPlatform.Services;
using System.Security.Claims;

namespace SmokingCessationSupportPlatform.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserService _userService;

        public IndexModel(ILogger<IndexModel> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var userRoleClaim = User.FindFirst(ClaimTypes.Role);

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                _logger.LogWarning("User ID claim not found or invalid. Redirecting to login.");
                return RedirectToPage("/Account/Login");
            }

            if (userRoleClaim == null)
            {
                _logger.LogWarning("User role claim not found. Redirecting to login.");
                return RedirectToPage("/Account/Login");
            }

            if (userRoleClaim.Value.Equals("Coach", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToPage("/Coach/CoachDashboard");
            }

            if (userRoleClaim.Value.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToPage("/Admin/Dashboard");
            }

            // Nếu là User thông thường
            var member = await _userService.GetUserWithProgressAsync(userId);
            if (member != null)
            {
                ViewData["Member"] = member;

                var quitProgressList = member.QuitProgresses?
                    .OrderByDescending(q => q.ReportDate)
                    .ToList();

                var achievementList = member.UserAchievements?
                        .Where(ua => ua.Achievement != null)
                        .Select(ua => ua.Achievement)
                        .OrderByDescending(a => a.AchievementId)
                        .ToList();

                var motivationalQuotes = new List<string>
                {
                    "Every day without smoking is a victory. Keep going!",
                    "You’re stronger than your cravings.",
                    "Breathe deeply – freedom is in your lungs now.",
                    "Stay focused. You’re rewriting your future.",
                    "One step at a time. You’ve got this!"
                };

                var random = new Random();
                var selectedQuote = motivationalQuotes[random.Next(motivationalQuotes.Count)];

                ViewData["QuitProgressList"] = quitProgressList;
                ViewData["Achievements"] = achievementList;
                ViewData["MotivationalQuote"] = selectedQuote;
            }

            return Page();


        }
    }
}
