using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.Services.Interfaces;
using System.Security.Claims;

namespace SmokingCessationSupportPlatform.Web.Pages.QuitProcess
{
    public class IndexModel : PageModel
    {
        private readonly IQuitProcessService _QuitProcessService;
        [BindProperty]
        public QuitProgress Progress { get; set; }

        public IndexModel(IQuitProcessService quitProcessService)
        {
            _QuitProcessService = quitProcessService;
        }
        public void OnGet()
        {
            Progress = new QuitProgress();
        }

        public IActionResult OnPost()
        {

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                
                ModelState.AddModelError(string.Empty, "Không thể xác định ID người dùng.");
                return Page();
            }

            QuitProgress quitProgress = new QuitProgress
            {
                UserId = userId,
                ReportDate = DateTime.Now,
                CigarettesSmoked = Progress.CigarettesSmoked,
                HealthStatus = Progress.HealthStatus,
                MoneySaved = (decimal)(Progress.CigarettesSmoked * 1250),
                DaysSmokingFree = 1,
                Notes = Progress.Notes
            };

            if (quitProgress != null)
            {
                _QuitProcessService.CreateQuitProcess(quitProgress);
                return RedirectToPage("/QuitProcess/ListQuitProgress");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Không thể tạo tiến trình bỏ thuốc lá.");
                return Page();
            }
               
        }

    }
}
