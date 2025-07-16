using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SmokingCessationSupportPlatform.Web.Pages.FeedbackMember
{
    public class CreateFeedbackModel : PageModel
    {
        private readonly IFeedbackService _feedbackService;

        public CreateFeedbackModel(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [BindProperty]
        public InputModel FeedbackInput { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Lo?i ph?n h?i là b?t bu?c.")]
            public string FeedbackType { get; set; }

            [Required(ErrorMessage = "Ch? ?? là b?t bu?c.")]
            public string Subject { get; set; }

            [Required(ErrorMessage = "N?i dung là b?t bu?c.")]
            public string Message { get; set; }
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var feedback = new Feedback
            {
                FeedbackType = FeedbackInput.FeedbackType,
                Subject = FeedbackInput.Subject,
                Message = FeedbackInput.Message,
                FeedbackDate = DateTime.Now,
                Status = "New",
                UserId = GetCurrentUserIdOrNull()
            };

            _feedbackService.CreateFeedback(feedback);

            return RedirectToPage("/Index");
        }

        private int? GetCurrentUserIdOrNull()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var id))
            {
                return id;
            }
            return null;
        }
    }
}
