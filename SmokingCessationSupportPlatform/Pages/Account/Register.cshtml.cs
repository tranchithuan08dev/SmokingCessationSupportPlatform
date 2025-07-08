using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokingCessationSupportPlatform.Services;
using System.ComponentModel.DataAnnotations;

namespace SmokingCessationSupportPlatform.Web.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IAccountService _accountService;

        public RegisterModel(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [BindProperty]
        public RegisterViewModel Input { get; set; } = new();

        public string Message { get; set; } = string.Empty;

        public class RegisterViewModel
        {
            [Required(ErrorMessage = "Username is required.")]
            [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
            public string Username { get; set; } = string.Empty;

            [Required(ErrorMessage = "Email is required.")]
            [EmailAddress(ErrorMessage = "Invalid email format.")]
            [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "Password is required.")]
            [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
            [DataType(DataType.Password)] 
            public string Password { get; set; } = string.Empty;

            [Required(ErrorMessage = "Confirm Password is required.")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; } = string.Empty;

            [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters.")]
            public string? FullName { get; set; } 

            [DataType(DataType.Date)] 
            [Display(Name = "Date of Birth")] 
            public DateOnly? DateOfBirth { get; set; } 

            [StringLength(10, ErrorMessage = "Gender cannot exceed 10 characters.")]
            public string? Gender { get; set; } 
        }
        public void OnGet()
        {
            Message = string.Empty;

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                var registeredUser = await _accountService.RegisterUserAsync(
                    Input.Username,
                    Input.Email,
                    Input.Password,
                    Input.FullName,
                    Input.DateOfBirth,
                    Input.Gender
                );

                if (registeredUser != null)
                {
                    Message = "Registration successful! You can now log in.";
                    return RedirectToPage("/Account/Login", new { message = "Registration successful! Please log in." });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");
                    Message = "Registration failed. Please try again.";
                    return Page();
                }
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                Message = ex.Message;
                return Page(); 
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred during registration. Please try again.");
                Message = "An unexpected error occurred during registration. Please try again.";
                Console.WriteLine(ex);
                return Page();
            }
        }
    }
}
