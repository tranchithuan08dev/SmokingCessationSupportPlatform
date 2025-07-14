using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.Services;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;

namespace SmokingCessationSupportPlatform.Web.Pages.Coach
{
    public class ChatModel : PageModel
    {
        private readonly IChatService _chatService;
        private readonly ILogger<ChatModel> _logger;

        public Conversation Conversation { get; set; } 

        [BindProperty]
        [Required(ErrorMessage = "Tin nhắn không được để trống.")]
        public string NewMessageContent { get; set; } = string.Empty;

        [BindProperty] 
        public int ConversationId { get; set; }

        public int CoachId { get; set; }

        public ChatModel(IChatService chatService, ILogger<ChatModel> logger)
        {
            _chatService = chatService;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(int conversationId)
        {
            var coachIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (coachIdClaim == null || !int.TryParse(coachIdClaim.Value, out int currentCoachId))
            {
                _logger.LogWarning("OnGetAsync: Coach ID claim not found or invalid. Redirecting to login.");
                return RedirectToPage("/Auth/Login");
            }
            CoachId = currentCoachId;
            _logger.LogInformation($"OnGetAsync: CoachId: {CoachId}, ConversationId from URL: {conversationId}");

            Conversation = await _chatService.GetConversationByIdAsync(conversationId);
            _logger.LogInformation($"OnGetAsync: Conversation retrieved: {Conversation != null}");

            if (Conversation == null || Conversation.CoachId != CoachId)
            {
                _logger.LogWarning($"OnGetAsync: Conversation not found or unauthorized. ConversationId: {conversationId}, CoachId in conversation: {Conversation?.CoachId}, Current CoachId: {CoachId}");
                return NotFound();
            }

            this.ConversationId = conversationId; 

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogInformation("OnPostAsync: Form submitted.");
            _logger.LogInformation($"OnPostAsync: ConversationId from form: {ConversationId}"); 
            _logger.LogInformation($"OnPostAsync: NewMessageContent from form: '{NewMessageContent}'");

            var coachIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (coachIdClaim == null || !int.TryParse(coachIdClaim.Value, out int currentCoachId))
            {
                _logger.LogWarning("OnPostAsync: Coach ID claim not found or invalid during POST. Redirecting to login.");
                return RedirectToPage("/Auth/Login");
            }
            CoachId = currentCoachId;
            _logger.LogInformation($"OnPostAsync: Current CoachId: {CoachId}");


            if (!ModelState.IsValid)
            {
                _logger.LogWarning("OnPostAsync: ModelState is NOT valid.");
                foreach (var modelStateEntry in ModelState.Values)
                {
                    foreach (var error in modelStateEntry.Errors)
                    {
                        _logger.LogError($"OnPostAsync: Validation Error: {error.ErrorMessage}");
                    }
                }
                foreach (var key in ModelState.Keys)
                {
                    if (ModelState[key].Errors.Any())
                    {
                        _logger.LogError($"OnPostAsync: Validation Error for {key}: {ModelState[key].Errors.First().ErrorMessage}");
                    }
                }
                Conversation = await _chatService.GetConversationByIdAsync(this.ConversationId); 
                return Page();
            }
            _logger.LogInformation("OnPostAsync: ModelState is valid.");

            var existingConversation = await _chatService.GetConversationByIdAsync(this.ConversationId);
            _logger.LogInformation($"OnPostAsync: Existing conversation retrieved: {existingConversation != null}");

            if (existingConversation == null || existingConversation.CoachId != CoachId)
            {
                _logger.LogWarning($"OnPostAsync: Existing conversation not found or unauthorized. ConversationId: {this.ConversationId}, CoachId in conversation: {existingConversation?.CoachId}, Current CoachId: {CoachId}");
                return NotFound();
            }

            _logger.LogInformation($"OnPostAsync: Sending message: From CoachId {CoachId} to UserId {existingConversation.UserId} in ConversationId {existingConversation.ConversationId} with content: '{NewMessageContent}'");
            var sentMessage = await _chatService.SendMessageAsync(
                existingConversation.ConversationId,
                CoachId,
                "Coach",
                existingConversation.UserId,
                "User",
                NewMessageContent
            );
            _logger.LogInformation($"OnPostAsync: Message sent. SentMessageViewModel is null: {sentMessage == null}");

            return RedirectToPage("/Coach/Chat", new { conversationId = existingConversation.ConversationId });
        }
    }
}