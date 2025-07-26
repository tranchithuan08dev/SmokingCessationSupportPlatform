using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.Services;
using System.Security.Claims;

namespace SmokingCessationSupportPlatform.Web.Pages.Coach
{
    public class MessageModel : PageModel
    {
        private readonly IChatService _chatService;

        public List<Conversation> Conversations { get; set; }

        public MessageModel(IChatService chatService /*, IUserRepository userRepository */)
        {
            _chatService = chatService;
            // _userRepository = userRepository;
        }


        public async Task<IActionResult> OnGetAsync()
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            if (role != "Coach")
            {
                return Redirect("Account/AccessDenied");
            }

            var coachIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (coachIdClaim == null)
            {
                return RedirectToPage("/Account/Login"); 
            }

            if (!int.TryParse(coachIdClaim.Value, out int coachId))
            {

                return RedirectToPage("/Error"); 
            }

            Conversations = await _chatService.GetCoachConversationsAsync(coachId);


            return Page(); 
        }
    }
}
