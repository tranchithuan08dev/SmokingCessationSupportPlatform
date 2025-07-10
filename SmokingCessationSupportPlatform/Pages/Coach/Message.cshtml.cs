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
        // Có th? c?n IUserRepository n?u mu?n hi?n th? tên user thay v? ch? ID
        // private readonly IUserRepository _userRepository;

        // Thu?c tính ð? lýu tr? danh sách cu?c tr? chuy?n
        public List<Conversation> Conversations { get; set; }

        public MessageModel(IChatService chatService /*, IUserRepository userRepository */)
        {
            _chatService = chatService;
            // _userRepository = userRepository;
        }

        // Phýõng th?c này s? ðý?c g?i khi trang ðý?c yêu c?u (GET request)
        // B? tham s? coachId kh?i OnGetAsync v? chúng ta s? l?y nó t? Claims
        public async Task<IActionResult> OnGetAsync()
        {
            // L?y CoachId t? thông tin ngý?i dùng ð? xác th?c (Claims Principal)
            // User là m?t thu?c tính có s?n trong PageModel, ð?i di?n cho Claims Principal c?a ngý?i dùng hi?n t?i.
            var coachIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (coachIdClaim == null)
            {
                // Ngý?i dùng chýa ðãng nh?p ho?c không có ClaimTypes.NameIdentifier
                // Chuy?n hý?ng v? trang ðãng nh?p ho?c trang l?i
                return RedirectToPage("/Auth/Login"); // Thay th? b?ng ðý?ng d?n trang ðãng nh?p c?a b?n
            }

            if (!int.TryParse(coachIdClaim.Value, out int coachId))
            {
                // Giá tr? c?a ClaimTypes.NameIdentifier không ph?i là s? nguyên h?p l?
                // X? l? l?i ho?c chuy?n hý?ng
                return RedirectToPage("/Error"); // Ho?c m?t trang l?i khác
            }

            // L?y danh sách cu?c tr? chuy?n c?a hu?n luy?n viên
            Conversations = await _chatService.GetCoachConversationsAsync(coachId);


            return Page(); // Tr? v? trang (render HTML)
        }
    }
}
