using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.Repositories;

namespace SmokingCessationSupportPlatform.Web.Pages.Admin
{
    public class AchievementsModel : PageModel
    {
        private readonly AchievementRepository _achievementRepo;

        public AchievementsModel(AchievementRepository achievementRepo)
        {
            _achievementRepo = achievementRepo;
        }

        public List<Achievement> AchievementList { get; set; } = new();

        public void OnGet()
        {
            AchievementList = _achievementRepo.GetAll();
        }

        public IActionResult OnPostCreate(string AchievementName, string? Description, string? Criteria, string? IconUrl)
        {
            var newAchievement = new Achievement
            {
                AchievementName = AchievementName,
                Description = Description,
                Criteria = Criteria,
                IconUrl = IconUrl
            };

            _achievementRepo.Add(newAchievement);
            return RedirectToPage();
        }

        public IActionResult OnPostDeleteAsync()
        {
            if (!int.TryParse(Request.Form["AchievementId"], out int id))
            {
                return BadRequest();
            }

            _achievementRepo.Delete(id); // ← thay bằng service thật
            return RedirectToPage();
        }
    }
}
