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
        // C� th? c?n IUserRepository n?u mu?n hi?n th? t�n user thay v? ch? ID
        // private readonly IUserRepository _userRepository;

        // Thu?c t�nh �? l�u tr? danh s�ch cu?c tr? chuy?n
        public List<Conversation> Conversations { get; set; }

        public MessageModel(IChatService chatService /*, IUserRepository userRepository */)
        {
            _chatService = chatService;
            // _userRepository = userRepository;
        }

        // Ph��ng th?c n�y s? ��?c g?i khi trang ��?c y�u c?u (GET request)
        // B? tham s? coachId kh?i OnGetAsync v? ch�ng ta s? l?y n� t? Claims
        public async Task<IActionResult> OnGetAsync()
        {
            // L?y CoachId t? th�ng tin ng�?i d�ng �? x�c th?c (Claims Principal)
            // User l� m?t thu?c t�nh c� s?n trong PageModel, �?i di?n cho Claims Principal c?a ng�?i d�ng hi?n t?i.
            var coachIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (coachIdClaim == null)
            {
                // Ng�?i d�ng ch�a ��ng nh?p ho?c kh�ng c� ClaimTypes.NameIdentifier
                // Chuy?n h�?ng v? trang ��ng nh?p ho?c trang l?i
                return RedirectToPage("/Auth/Login"); // Thay th? b?ng ��?ng d?n trang ��ng nh?p c?a b?n
            }

            if (!int.TryParse(coachIdClaim.Value, out int coachId))
            {
                // Gi� tr? c?a ClaimTypes.NameIdentifier kh�ng ph?i l� s? nguy�n h?p l?
                // X? l? l?i ho?c chuy?n h�?ng
                return RedirectToPage("/Error"); // Ho?c m?t trang l?i kh�c
            }

            // L?y danh s�ch cu?c tr? chuy?n c?a hu?n luy?n vi�n
            Conversations = await _chatService.GetCoachConversationsAsync(coachId);


            return Page(); // Tr? v? trang (render HTML)
        }
    }
}
