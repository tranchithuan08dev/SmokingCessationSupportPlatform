using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.Services;

namespace SmokingCessationSupportPlatform.Web.Pages.Admin
{
    public class EditAchievementModel : PageModel
    {
        private readonly AchievementService _achievementService;

        public EditAchievementModel(AchievementService achievementService)
        {
            _achievementService = achievementService;
        }

        [BindProperty]
        public Achievement Achievement { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Achievement = await _achievementService.GetByIdAsync(id);
            if (Achievement == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _achievementService.UpdateAchievementAsync(Achievement);
            return RedirectToPage("/Admin/Achievements");
        }
    }
}
