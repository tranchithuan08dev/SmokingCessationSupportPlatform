using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokingCessationSupportPlatform.Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace SmokingCessationSupportPlatform.Web.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IAccountService _accountService;

        public LoginModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public LoginViewModel Input { get; set; } = new();
        public string ErrorMessage { get; set; }

        public class LoginViewModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; } = string.Empty;

            [Required]
            public string Password { get; set; } = string.Empty;
        }

        public void OnGet()
        {


        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var user = _accountService.LoginByEmailAndPassword(Input.Email, Input.Password);

                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim("FullName", user.FullName ?? string.Empty),
                    };


                    if (string.IsNullOrEmpty(user.UserRole) || user.UserRole.Equals("Member", StringComparison.OrdinalIgnoreCase))
                    {
                        claims.Add(new Claim(ClaimTypes.Role, "User"));
                    }
                    else
                    {
                        claims.Add(new Claim(ClaimTypes.Role, user.UserRole));
                    }


                    var claimsIdentity = new ClaimsIdentity(
                       claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                    };


                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    switch (user.UserRole)
                    {
                        case "Member":
                            TempData["ShowWelcomeModal"] = true;
                            return RedirectToPage("/Index");
                        case "Admin":
                            return RedirectToPage("/Admin/Dashboard");
                        case "Coach":
                            return RedirectToPage("/Coach/CoachDashboard");
                        default:
                            return RedirectToPage("/Index");
                    }

                    return RedirectToPage("/Index");
                }
                else
                {
                    ErrorMessage = "Invalid login attempt. Please check your email and password.";
                    Console.WriteLine("Invalid login attempt. Please check your email and password");
                    return Page();
                }
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ErrorMessage = ex.Message;
                Console.WriteLine(ex);
                return Page();
            }
            catch (Exception ex)
            {
                ErrorMessage = "An unexpected error occurred during login. Please try again.";
                Console.WriteLine(ex);
                return Page();
            }
        }
    }
}
