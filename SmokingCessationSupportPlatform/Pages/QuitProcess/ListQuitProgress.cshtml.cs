using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.Services.Interfaces;
using System.Security.Claims;

namespace SmokingCessationSupportPlatform.Web.Pages.QuitProcess
{
    public class ListQuitProgressModel : PageModel
    {

        private readonly IQuitProcessService _quitProgressService;

        public List<QuitProgress> QuitProgressList { get; set; } = new();
        public int TotalCigarettesSmoked { get; set; }
        public decimal TotalMoneySaved { get; set; }
        public int TotalDaysSmokingFree { get; set; }
        public ListQuitProgressModel(IQuitProcessService quitProgressService)
        {
            _quitProgressService = quitProgressService;
        }
        [TempData]
        public string ErrorMessage { get; set; }
        public void OnGet()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

                int userId;

                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out userId))
                {
                    QuitProgressList = _quitProgressService.GetListQuitProgresses(userId);

                    if (QuitProgressList.Any())
                    {
                        TotalCigarettesSmoked = QuitProgressList.Sum(qp => qp.CigarettesSmoked ?? 0);
                        TotalMoneySaved = QuitProgressList.Sum(qp => qp.MoneySaved ?? 0m);
                        TotalDaysSmokingFree = QuitProgressList.Max(qp => qp.DaysSmokingFree ?? 0);
                    }
                }
                else
                {
                    ErrorMessage = "Không thể xác định ID người dùng. Vui lòng đăng nhập lại.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Đã xảy ra lỗi khi tải tiến trình bỏ thuốc: {ex.Message}";
            }
        }
    }
    
}
