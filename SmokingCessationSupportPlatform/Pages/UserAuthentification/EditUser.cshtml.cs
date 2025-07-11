using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.Repositories;
using SmokingCessationSupportPlatform.Services;
using System.Security.Claims;

namespace SmokingCessationSupportPlatform.Web.Pages.UserAuthentification
{
    public class EditUserModel : PageModel
    {
        private readonly IUserAuthentification _editUserModel;
        private readonly ILogger<EditUserModel> _logger;

        [BindProperty]
        public  User user { get; set; } = new User();

        public EditUserModel(IUserAuthentification userService, ILogger<EditUserModel> logger)
        {
            _editUserModel = userService;
            _logger = logger;
        }

        public async Task<IActionResult>  OnGetAsync()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            int id = 0;
            if (userId == null || !int.TryParse(userId.Value, out id))
            {
                _logger.LogWarning("OnGet: User ID claim not found or invalid. Redirecting to login.");
                TempData["ErrorMessage"] = "Bạn cần đăng nhập để chỉnh sửa hồ sơ.";
                RedirectToPage("/Account/Login");
            }
            user = await _editUserModel.GetUserByIdAsync(id);
            if(user == null)
            {
                _logger.LogWarning("OnGet: User not found with ID {UserId}", id);
                TempData["ErrorMessage"] = "Không tìm thấy người dùng.";
                return RedirectToPage("/Index");
            }else
            {
                return Page();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int currentUserId))
            {
                _logger.LogWarning("OnPostAsync: User ID claim not found or invalid during POST. Redirecting to login.");
                TempData["ErrorMessage"] = "Phiên đăng nhập của bạn đã hết hạn. Vui lòng đăng nhập lại.";
                return RedirectToPage("/Account/Login");
            }

            var userToUpdate = await _editUserModel.GetUserByIdAsync(currentUserId);

            if (userToUpdate == null)
            {
                _logger.LogError($"OnPostAsync: User with ID {currentUserId} not found for update.");
                TempData["ErrorMessage"] = "Không tìm thấy hồ sơ để cập nhật.";
                return RedirectToPage("/Error");
            }
            userToUpdate.FullName = user.FullName;
            userToUpdate.Email = user.Email;
            userToUpdate.DateOfBirth = user.DateOfBirth;
            userToUpdate.Gender = user.Gender;

            if (ModelState.ContainsKey("user.PasswordHash"))
            {
                ModelState.Remove("user.PasswordHash");
            }
            if (ModelState.ContainsKey("PasswordHash"))
            {
                ModelState.Remove("PasswordHash");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("OnPostAsync: ModelState is invalid after manual update.");
                foreach (var modelStateEntry in ModelState.Values)
                {
                    foreach (var error in modelStateEntry.Errors)
                    {
                        _logger.LogError($"OnPostAsync: Validation Error: {error.ErrorMessage}");
                    }
                }
                TempData["ErrorMessage"] = "Thông tin nhập vào không hợp lệ. Vui lòng kiểm tra lại.";
                this.user = userToUpdate;
                return Page();
            }

            try
            {
                await _editUserModel.UpdateUserAsync(userToUpdate);
                _logger.LogInformation($"User profile for ID {currentUserId} updated successfully.");
                TempData["SuccessMessage"] = "Hồ sơ đã được cập nhật thành công.";
                return RedirectToPage("/UserAuthentification/EditUser");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"OnPostAsync: Error updating user profile for ID {currentUserId}.");
                TempData["ErrorMessage"] = "Đã xảy ra lỗi khi cập nhật hồ sơ. Vui lòng thử lại sau.";
                this.user = userToUpdate; 
                return Page();
            }
        }

    }
}
