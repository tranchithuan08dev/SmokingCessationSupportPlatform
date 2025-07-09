using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokingCessationSupportPlatform.BusinessObjects.DTO;
using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.Services;
using System.Security.Claims;

namespace SmokingCessationSupportPlatform.Web.Pages
{
    public class ChatModel : PageModel
    {
        private readonly IChatService _chatService;
        private readonly ILogger<ChatModel> _logger; // Ð? ghi log l?i

        [BindProperty] // Dùng ð? binding d? li?u t? form n?u c?n (ví d?: g?i tin nh?n)
        public string MessageContent { get; set; } = string.Empty;

        public int CurrentUserId { get; set; }
        public string CurrentUserType { get; set; } = string.Empty; // "User" ho?c "Coach"
        public int PartnerId { get; set; } // ID c?a ngý?i dùng/coach mà b?n ðang chat cùng
        public string PartnerType { get; set; } = string.Empty; // "User" ho?c "Coach"
        public int ConversationId { get; set; }

        public List<MessageViewModel> Messages { get; set; } = new List<MessageViewModel>();

        // Constructor ð? inject ChatService
        public ChatModel(IChatService chatService, ILogger<ChatModel> logger)
        {
            _chatService = chatService;
            _logger = logger;
        }

        // Handler cho GET request khi t?i trang chat
        public async Task<IActionResult> OnGetAsync(int partnerId, string partnerType)
        {
            // L?y thông tin ngý?i dùng hi?n t?i t? Claims
            // B?n c?n ði?u ch?nh logic này ð? l?y ðúng UserId/CoachId và UserType c?a b?n
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier); // Gi? s? UserId/CoachId ðý?c lýu trong NameIdentifier
            var userRoleClaim = User.FindFirst(ClaimTypes.Role);         // Gi? s? Role ðý?c lýu trong ClaimTypes.Role

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int currentId))
            {
                return RedirectToPage("/Account/Login"); // Ho?c tr? v? l?i
            }

            CurrentUserId = currentId;
            CurrentUserType = userRoleClaim?.Value ?? "Member"; // M?c ð?nh là "User" n?u không có role

            PartnerId = partnerId;
            PartnerType = partnerType;

            if (PartnerId == 0 || string.IsNullOrEmpty(PartnerType))
            {
                // X? l? trý?ng h?p không có partnerId ho?c partnerType (ví d?: hi?n th? danh sách các cu?c tr? chuy?n)
                // Hi?n t?i, chúng ta s? ðõn gi?n chuy?n hý?ng v? m?t trang l?i ho?c trang danh sách chat
                return RedirectToPage("/Error"); // Ho?c trang danh sách chat c?a b?n
            }

            try
            {
                Conversation conversation;

                // Logic ð? t?o ho?c l?y ConversationId
                if ((CurrentUserType == "User" || CurrentUserType == "Member") && PartnerType == "Coach")
                {
                    conversation = await _chatService.GetOrCreateConversationAsync(CurrentUserId, PartnerId);
                }
                else if (CurrentUserType == "Coach" && (PartnerType == "User" || PartnerType == "Member"))
                {
                    conversation = await _chatService.GetOrCreateConversationAsync(PartnerId, CurrentUserId); // User là PartnerId, Coach là CurrentUserId
                }
                else
                {
                    // Trý?ng h?p User chat v?i User ho?c Coach chat v?i Coach (hi?n t?i không ðý?c h? tr? theo Conversation Model)
                    _logger.LogWarning($"Unsupported chat type: CurrentUserType={CurrentUserType}, PartnerType={PartnerType}");
                    return RedirectToPage("/Error", new { message = "Lo?i cu?c tr? chuy?n không ðý?c h? tr?." });
                }

                ConversationId = conversation.ConversationId;

                // L?y tin nh?n hi?n có
                Messages = await _chatService.GetConversationMessagesWithSenderInfoAsync(ConversationId);

                // Ðánh d?u tin nh?n là ð? ð?c cho ngý?i nh?n hi?n t?i
                await _chatService.MarkMessagesAsReadAsync(ConversationId, CurrentUserId, CurrentUserType);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "L?i khi t?i trang chat.");
                return RedirectToPage("/Error", new { message = "Ð? x?y ra l?i khi t?i cu?c tr? chuy?n." });
            }

            return Page();
        }

        // Handler cho POST request khi g?i tin nh?n t? form (có th? thay th? b?ng SignalR tr?c ti?p)
        public async Task<IActionResult> OnPostSendMessageAsync(int conversationId, int fromId, string fromType, int toId, string toType)
        {
            if (string.IsNullOrWhiteSpace(MessageContent))
            {
                TempData["ErrorMessage"] = "Tin nh?n không ðý?c ð? tr?ng.";
                // T?i l?i trang v?i d? li?u hi?n có
                return await OnGetAsync(toId, toType);
            }

            try
            {
                var sentMessage = await _chatService.SendMessageAsync(conversationId, fromId, fromType, toId, toType, MessageContent);

                if (sentMessage != null)
                {
                    MessageContent = string.Empty; // Xóa n?i dung tin nh?n sau khi g?i
                }
                else
                {
                    TempData["ErrorMessage"] = "Không th? g?i tin nh?n.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "L?i khi g?i tin nh?n.");
                TempData["ErrorMessage"] = "Ð? x?y ra l?i khi g?i tin nh?n.";
            }

            // Chuy?n hý?ng tr? l?i trang chat ð? t?i l?i tin nh?n ð? g?i
            return RedirectToPage("/Chat", new { partnerId = toId, partnerType = toType });
            // Ho?c, n?u mu?n tránh redirect, b?n c?n c?p nh?t Messages list và tr? v? Page()
            // Tuy nhiên, vi?c redirect là cách ðõn gi?n hõn ð? ð?m b?o tr?ng thái UI ðý?c ð?ng b?
        }
    }
}
