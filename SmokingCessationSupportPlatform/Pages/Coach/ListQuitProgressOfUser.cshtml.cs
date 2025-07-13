using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.Services.Interfaces;
using System.Security.Claims;

namespace SmokingCessationSupportPlatform.Web.Pages.Coach
{
    public class ListQuitProgressOfUserModel : PageModel
    {
        private readonly IQuitProcessService _quitProgressService;

        [BindProperty(SupportsGet = true)]
        public int UserID { get; set; }
        public List<QuitProgress> QuitProgressList { get; set; } = new();
        public int TotalCigarettesSmoked { get; set; }
        public decimal TotalMoneySaved { get; set; }
        public int TotalDaysSmokingFree { get; set; }
        public ListQuitProgressOfUserModel(IQuitProcessService quitProgressService)
        {
            _quitProgressService = quitProgressService;
        }
        [TempData]
        public string ErrorMessage { get; set; }
        public void OnGet()
        {
            try
            {
             QuitProgressList = _quitProgressService.GetListQuitProgresses(UserID);

             if (QuitProgressList.Any())
              {
                        TotalCigarettesSmoked = QuitProgressList.Sum(qp => qp.CigarettesSmoked ?? 0);
                        TotalMoneySaved = QuitProgressList.Sum(qp => qp.MoneySaved ?? 0m);
                        TotalDaysSmokingFree = QuitProgressList.Max(qp => qp.DaysSmokingFree ?? 0);
               }
              
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Đã xảy ra lỗi khi tải tiến trình bỏ thuốc: {ex.Message}";
            }
        }
    }
}

