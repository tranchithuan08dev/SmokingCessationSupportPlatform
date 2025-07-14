using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokingCessationSupportPlatform.BusinessObjects.DTO;
using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace SmokingCessationSupportPlatform.Web.Pages
{
    public class ChatModel : PageModel
    {
        private readonly IChatService _chatService;
        private readonly ILogger<ChatModel> _logger;

        [BindProperty]
        [Required(ErrorMessage = "Tin nhắn không được để trống.")]
        public string MessageContent { get; set; } = string.Empty;

        public int CurrentUserId { get; set; }
        public string CurrentUserType { get; set; } = string.Empty; 

        [BindProperty]
        public int PartnerId { get; set; } 
        [BindProperty]
        public string PartnerType { get; set; } = string.Empty; 

        [BindProperty] 
        public int ConversationId { get; set; } 

        public List<MessageViewModel> Messages { get; set; } = new List<MessageViewModel>();

        public ChatModel(IChatService chatService, ILogger<ChatModel> logger)
        {
            _chatService = chatService;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(int partnerId, string partnerType)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var userRoleClaim = User.FindFirst(ClaimTypes.Role);

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int currentId))
            {
                _logger.LogWarning("OnGetAsync: User ID claim not found or invalid. Redirecting to login.");
                return RedirectToPage("/Account/Login");
            }

            CurrentUserId = currentId;
            CurrentUserType = userRoleClaim?.Value ?? "User";
            if (CurrentUserType.Equals("Member", StringComparison.OrdinalIgnoreCase)) CurrentUserType = "User";
            if (CurrentUserType.Equals("Admin", StringComparison.OrdinalIgnoreCase)) CurrentUserType = "Coach";

            this.PartnerId = partnerId;
            this.PartnerType = partnerType;

            if (this.PartnerId == 0 || string.IsNullOrEmpty(this.PartnerType) || (!this.PartnerType.Equals("User", StringComparison.OrdinalIgnoreCase) && !this.PartnerType.Equals("Coach", StringComparison.OrdinalIgnoreCase)))
            {
                _logger.LogWarning($"OnGetAsync: Missing or invalid partnerId or partnerType. PartnerId: {this.PartnerId}, PartnerType: {this.PartnerType}");
                if (CurrentUserType == "Coach")
                {
                    return RedirectToPage("/Coach/Message");
                }
                else 
                {
                    return RedirectToPage("/User/FindCoach");
                }
            }

            try
            {
                Conversation conversation;

                if (CurrentUserType == "User" && PartnerType == "Coach")
                {
                    conversation = await _chatService.GetOrCreateConversationAsync(CurrentUserId, PartnerId);
                }
                else if (CurrentUserType == "Coach" && PartnerType == "User")
                {
                    conversation = await _chatService.GetOrCreateConversationAsync(PartnerId, CurrentUserId);
                }
                else
                {
                    _logger.LogWarning($"OnGetAsync: Unsupported chat type: CurrentUserType={CurrentUserType}, PartnerType={PartnerType}");
                    return RedirectToPage("/Error", new { message = "Loại cuộc trò chuyện không được hỗ trợ." });
                }

                this.ConversationId = conversation.ConversationId; 

                Messages = await _chatService.GetConversationMessagesWithSenderInfoAsync(this.ConversationId);
                await _chatService.MarkMessagesAsReadAsync(this.ConversationId, CurrentUserId, CurrentUserType);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "OnGetAsync: Lỗi khi tải trang chat.");
                return RedirectToPage("/Error", new { message = "Đã xảy ra lỗi khi tải cuộc trò chuyện." });
            }

            return Page();
        }

        //public async Task<IActionResult> OnPostAsync()
        //{
        //    _logger.LogInformation("OnPostAsync: Form submitted.");
        //    _logger.LogInformation($"OnPostAsync: MessageContent from form: '{MessageContent}'");

        //    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        //    var userRoleClaim = User.FindFirst(ClaimTypes.Role);

        //    if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int currentId))
        //    {
        //        _logger.LogWarning("OnPostAsync: User ID claim not found or invalid during POST. Redirecting to login.");
        //        return RedirectToPage("/Account/Login");
        //    }

        //    CurrentUserId = currentId;
        //    CurrentUserType = userRoleClaim?.Value ?? "User";
        //    if (CurrentUserType.Equals("Member", StringComparison.OrdinalIgnoreCase)) CurrentUserType = "User";
        //    if (CurrentUserType.Equals("Admin", StringComparison.OrdinalIgnoreCase)) CurrentUserType = "Coach";

        //    _logger.LogInformation($"OnPostAsync: ConversationId: {ConversationId}, PartnerId: {PartnerId}, PartnerType: {PartnerType}");

        //    if (!ModelState.IsValid)
        //    {
        //        _logger.LogWarning("OnPostAsync: ModelState is NOT valid.");
        //        foreach (var modelStateEntry in ModelState.Values)
        //        {
        //            foreach (var error in modelStateEntry.Errors)
        //            {
        //                _logger.LogError($"OnPostAsync: Validation Error: {error.ErrorMessage}");
        //            }
        //        }

        //        return await OnGetAsync(PartnerId, PartnerType);
        //    }
        //    _logger.LogInformation("OnPostAsync: ModelState is valid.");

        //    try
        //    {
        //        var sentMessage = await _chatService.SendMessageAsync(
        //            ConversationId,
        //            CurrentUserId,
        //            CurrentUserType,
        //            PartnerId,
        //            PartnerType,
        //            MessageContent
        //        );

        //        if (sentMessage != null)
        //        {
        //            MessageContent = string.Empty; 
        //            _logger.LogInformation($"OnPostAsync: Message sent successfully. MessageId: {sentMessage.MessageId}");
        //        }
        //        else
        //        {
        //            TempData["ErrorMessage"] = "Không thể gửi tin nhắn. Dịch vụ trả về null.";
        //            _logger.LogError("OnPostAsync: SendMessageAsync returned null.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "OnPostAsync: Lỗi khi gửi tin nhắn.");
        //        TempData["ErrorMessage"] = "Đã xảy ra lỗi khi gửi tin nhắn.";
        //    }
        //    MessageContent = string.Empty;
        //    Messages = await _chatService.GetConversationMessagesWithSenderInfoAsync(this.ConversationId);
        //    return Page();
        //}
    }
}
