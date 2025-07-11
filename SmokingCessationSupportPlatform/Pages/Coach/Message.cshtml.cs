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
        // private readonly IUserRepository _userRepository;

        // Thu?c tính ð? lýu tr? danh sách cu?c tr? chuy?n
        public List<Conversation> Conversations { get; set; }

        public MessageModel(IChatService chatService /*, IUserRepository userRepository */)
        {
            _chatService = chatService;
            // _userRepository = userRepository;
        }


        public async Task<IActionResult> OnGetAsync()
        {

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
