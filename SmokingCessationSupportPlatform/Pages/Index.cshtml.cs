using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.Services.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;

namespace SmokingCessationSupportPlatform.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBlogPostService _blogPostService;
        private readonly IFeedbackService _feedbackService;

        public List<BlogPost> ListBlogPosts { get; set; } = new List<BlogPost>();
        public List<Feedback> ListFeedbacks { get; set; } = new List<Feedback>();


        public IndexModel(ILogger<IndexModel> logger, IBlogPostService blogPostService, IFeedbackService feedbackService)
        {
            _logger = logger;
            _blogPostService = blogPostService;
            _feedbackService = feedbackService;
        }

        public IActionResult OnGet()
        {
            // L?y danh sách blog
            ListBlogPosts = _blogPostService.GetAllBlogPosts();
            ListFeedbacks = _feedbackService.GetAllFeedbacks();

            // L?y thông tin user t? claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var userRoleClaim = User.FindFirst(ClaimTypes.Role);

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int currentId))
            {
                _logger.LogWarning("User ID claim not found or invalid. Redirecting to login.");
                return RedirectToPage("/Account/Login");
            }

            if (userRoleClaim == null)
            {
                _logger.LogWarning("User role claim not found. Redirecting to login.");
                return RedirectToPage("/Account/Login");
            }

            if (userRoleClaim.Value.Equals("Coach", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToPage("/Coach/CoachDashboard");
            }
            return Page();
        }

    }
}
