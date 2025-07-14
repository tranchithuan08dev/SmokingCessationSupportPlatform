using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokingCessationSupportPlatform.Services;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations; // Thêm using này
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging; // Thêm ILogger

namespace SmokingCessationSupportPlatform.Web.Pages.Account
{
    public class ChangePasswordModel : PageModel
    {
        private readonly IAccountService _accountService; // Đổi thành private readonly
        private readonly ILogger<ChangePasswordModel> _logger; // Thêm ILogger

        public ChangePasswordModel(IAccountService accountService, ILogger<ChangePasswordModel> logger) // Inject ILogger
        {
            _accountService = accountService;
            _logger = logger;
        }

        [BindProperty]
        public ChangePasswordViewModel Input { get; set; } = new();

        // Thêm TempData để hiển thị thông báo thành công/thất bại
        [TempData]
        public string? SuccessMessage { get; set; }
        [TempData]
        public string? ErrorMessage { get; set; }


        public class ChangePasswordViewModel
        {
            [Required(ErrorMessage = "Mật khẩu hiện tại không được để trống.")]
            [DataType(DataType.Password)]
            [Display(Name = "Mật khẩu hiện tại")]
            public string CurrentPassword { get; set; } = string.Empty;

            [Required(ErrorMessage = "Mật khẩu mới không được để trống.")]
            [StringLength(100, ErrorMessage = "Mật khẩu mới phải có ít nhất {2} và tối đa {1} ký tự.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Mật khẩu mới")]
            public string NewPassword { get; set; } = string.Empty;

            [Required(ErrorMessage = "Xác nhận mật khẩu mới không được để trống.")]
            [DataType(DataType.Password)]
            [Display(Name = "Xác nhận mật khẩu mới")]
            [Compare("NewPassword", ErrorMessage = "Mật khẩu mới và xác nhận mật khẩu không khớp.")] // Tự động so sánh
            public string ConfirmNewPassword { get; set; } = string.Empty;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("OnPostAsync: ModelState is invalid.");
                return Page();
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                ErrorMessage = "Phiên đăng nhập của bạn đã hết hạn. Vui lòng đăng nhập lại.";
                return RedirectToPage("/Account/Login");
            }

            try
            {
                var result = await _accountService.ChangePassword(userId, Input.CurrentPassword, Input.NewPassword);
                if (result)
                {
                    SuccessMessage = "Mật khẩu đã được thay đổi thành công. Bạn sẽ được chuyển hướng về trang đăng nhập sau 3 giây."; 
                    return Page();
                }
                else
                {
                    ErrorMessage = "Đổi mật khẩu không thành công. Vui lòng kiểm tra lại mật khẩu hiện tại.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing password for user {UserId}", userId); 
                ErrorMessage = $"Đã xảy ra lỗi khi đổi mật khẩu. Vui lòng thử lại sau."; 
            }

            return Page(); 
        }
    }
}
