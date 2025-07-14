using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokingCessationSupportPlatform.Services;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.Web.Pages.Account
{
    public class ResetPasswordModel : PageModel
    {
        private readonly IAccountService _accountService;

        public ResetPasswordModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; } = false;

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; } = string.Empty;

            [Required]
            public string Token { get; set; } = string.Empty; // The reset token from the URL

            [Required(ErrorMessage = "New Password is required.")]
            [StringLength(100, MinimumLength = 6, ErrorMessage = "New Password must be at least 6 characters long.")]
            [DataType(DataType.Password)]
            public string NewPassword { get; set; } = string.Empty;

            [Required(ErrorMessage = "Confirm Password is required.")]
            [DataType(DataType.Password)]
            [Compare("NewPassword", ErrorMessage = "New password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; } = string.Empty;
        }

        // OnGet is called when the user clicks the reset link in the email
        public IActionResult OnGet(string email, string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                Message = "Invalid password reset link. Please try again.";
                IsSuccess = false;
                return Page();
            }

            Input.Email = email;
            Input.Token = token;
            Message = string.Empty; // Clear any previous messages
            IsSuccess = false;
            return Page();
        }

        // OnPost is called when the user submits the new password form
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var result = await _accountService.ResetPasswordAsync(Input.Email, Input.Token, Input.NewPassword); // Needs to be implemented

                if (result) 
                {
                    Message = "Your password has been successfully reset. You can now log in with your new password.";
                    IsSuccess = true;
                    // Optional: Redirect to login page after successful reset
                    // return RedirectToPage("/Account/Login", new { message = "Password reset successful! Please log in." });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to reset password. The link might be invalid or expired. Please try again.");
                    Message = "Failed to reset password. The link might be invalid or expired. Please try again.";
                    IsSuccess = false;
                }
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");
                Message = "An unexpected error occurred. Please try again.";
                IsSuccess = false;
                return Page();
            }
        }
    }
}