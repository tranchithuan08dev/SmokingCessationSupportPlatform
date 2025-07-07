using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokingCessationSupportPlatform.Services; // Assuming your IAccountService is here
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.Web.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly IAccountService _accountService; 
        private readonly IEmailService _emailService;     

        public ForgotPasswordModel(IAccountService accountService, IEmailService emailService)
        {
            _accountService = accountService;
            _emailService = emailService;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; } = false;

        public class InputModel
        {
            [Required(ErrorMessage = "Email is required.")]
            [EmailAddress(ErrorMessage = "Invalid email format.")]
            public string Email { get; set; } = string.Empty;
        }

        public void OnGet()
        {
            Message = string.Empty;
            IsSuccess = false;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Find the user by email
                var user = _accountService.GetUserByEmail(Input.Email);

                if (user == null)
                {
                    Message = "If an account with that email exists, a password reset link has been sent.";
                    IsSuccess = true;
                    return Page();
                }

 
                var token = await _accountService.GenerateAndSavePasswordResetTokenAsync(user.Email); 
                var resetLink = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { email = user.Email, token = token },
                    protocol: Request.Scheme 
                );

                if (resetLink == null)
                {
                    throw new Exception("Failed to generate password reset link.");
                }

                string subject = "Password Reset Request for Your Account";
                string body = $"Please reset your password by clicking on this link: <a href='{resetLink}'>Reset Password</a>";

                await _emailService.SendEmailAsync(Input.Email, subject, body);

                Message = "A password reset link has been sent to your email address.";
                IsSuccess = true;
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request. Please try again.");
                Message = "An error occurred while processing your request. Please try again.";
                IsSuccess = false;
                Console.WriteLine(ex);
                    return Page();
            }
        }
    }
}