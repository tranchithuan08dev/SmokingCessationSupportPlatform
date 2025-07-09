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
        private readonly ILogger<ChatModel> _logger; // �? ghi log l?i

        [BindProperty] // D�ng �? binding d? li?u t? form n?u c?n (v� d?: g?i tin nh?n)
        public string MessageContent { get; set; } = string.Empty;

        public int CurrentUserId { get; set; }
        public string CurrentUserType { get; set; } = string.Empty; // "User" ho?c "Coach"
        public int PartnerId { get; set; } // ID c?a ng�?i d�ng/coach m� b?n �ang chat c�ng
        public string PartnerType { get; set; } = string.Empty; // "User" ho?c "Coach"
        public int ConversationId { get; set; }

        public List<MessageViewModel> Messages { get; set; } = new List<MessageViewModel>();

        // Constructor �? inject ChatService
        public ChatModel(IChatService chatService, ILogger<ChatModel> logger)
        {
            _chatService = chatService;
            _logger = logger;
        }

        // Handler cho GET request khi t?i trang chat
        public async Task<IActionResult> OnGetAsync(int partnerId, string partnerType)
        {
            // L?y th�ng tin ng�?i d�ng hi?n t?i t? Claims
            // B?n c?n �i?u ch?nh logic n�y �? l?y ��ng UserId/CoachId v� UserType c?a b?n
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier); // Gi? s? UserId/CoachId ��?c l�u trong NameIdentifier
            var userRoleClaim = User.FindFirst(ClaimTypes.Role);         // Gi? s? Role ��?c l�u trong ClaimTypes.Role

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int currentId))
            {
                return RedirectToPage("/Account/Login"); // Ho?c tr? v? l?i
            }

            CurrentUserId = currentId;
            CurrentUserType = userRoleClaim?.Value ?? "Member"; // M?c �?nh l� "User" n?u kh�ng c� role

            PartnerId = partnerId;
            PartnerType = partnerType;

            if (PartnerId == 0 || string.IsNullOrEmpty(PartnerType))
            {
                // X? l? tr�?ng h?p kh�ng c� partnerId ho?c partnerType (v� d?: hi?n th? danh s�ch c�c cu?c tr? chuy?n)
                // Hi?n t?i, ch�ng ta s? ��n gi?n chuy?n h�?ng v? m?t trang l?i ho?c trang danh s�ch chat
                return RedirectToPage("/Error"); // Ho?c trang danh s�ch chat c?a b?n
            }

            try
            {
                Conversation conversation;

                // Logic �? t?o ho?c l?y ConversationId
                if ((CurrentUserType == "User" || CurrentUserType == "Member") && PartnerType == "Coach")
                {
                    conversation = await _chatService.GetOrCreateConversationAsync(CurrentUserId, PartnerId);
                }
                else if (CurrentUserType == "Coach" && (PartnerType == "User" || PartnerType == "Member"))
                {
                    conversation = await _chatService.GetOrCreateConversationAsync(PartnerId, CurrentUserId); // User l� PartnerId, Coach l� CurrentUserId
                }
                else
                {
                    // Tr�?ng h?p User chat v?i User ho?c Coach chat v?i Coach (hi?n t?i kh�ng ��?c h? tr? theo Conversation Model)
                    _logger.LogWarning($"Unsupported chat type: CurrentUserType={CurrentUserType}, PartnerType={PartnerType}");
                    return RedirectToPage("/Error", new { message = "Lo?i cu?c tr? chuy?n kh�ng ��?c h? tr?." });
                }

                ConversationId = conversation.ConversationId;

                // L?y tin nh?n hi?n c�
                Messages = await _chatService.GetConversationMessagesWithSenderInfoAsync(ConversationId);

                // ��nh d?u tin nh?n l� �? �?c cho ng�?i nh?n hi?n t?i
                await _chatService.MarkMessagesAsReadAsync(ConversationId, CurrentUserId, CurrentUserType);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "L?i khi t?i trang chat.");
                return RedirectToPage("/Error", new { message = "�? x?y ra l?i khi t?i cu?c tr? chuy?n." });
            }

            return Page();
        }

        // Handler cho POST request khi g?i tin nh?n t? form (c� th? thay th? b?ng SignalR tr?c ti?p)
        public async Task<IActionResult> OnPostSendMessageAsync(int conversationId, int fromId, string fromType, int toId, string toType)
        {
            if (string.IsNullOrWhiteSpace(MessageContent))
            {
                TempData["ErrorMessage"] = "Tin nh?n kh�ng ��?c �? tr?ng.";
                // T?i l?i trang v?i d? li?u hi?n c�
                return await OnGetAsync(toId, toType);
            }

            try
            {
                var sentMessage = await _chatService.SendMessageAsync(conversationId, fromId, fromType, toId, toType, MessageContent);

                if (sentMessage != null)
                {
                    MessageContent = string.Empty; // X�a n?i dung tin nh?n sau khi g?i
                }
                else
                {
                    TempData["ErrorMessage"] = "Kh�ng th? g?i tin nh?n.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "L?i khi g?i tin nh?n.");
                TempData["ErrorMessage"] = "�? x?y ra l?i khi g?i tin nh?n.";
            }

            // Chuy?n h�?ng tr? l?i trang chat �? t?i l?i tin nh?n �? g?i
            return RedirectToPage("/Chat", new { partnerId = toId, partnerType = toType });
            // Ho?c, n?u mu?n tr�nh redirect, b?n c?n c?p nh?t Messages list v� tr? v? Page()
            // Tuy nhi�n, vi?c redirect l� c�ch ��n gi?n h�n �? �?m b?o tr?ng th�i UI ��?c �?ng b?
        }
    }
}
