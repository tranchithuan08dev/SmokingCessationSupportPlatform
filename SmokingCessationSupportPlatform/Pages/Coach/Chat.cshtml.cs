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

        public Conversation Conversation { get; set; } // This is for display, populated in OnGet/OnPost error path

        [BindProperty]
        [Required(ErrorMessage = "Tin nh?n không ðý?c ð? tr?ng.")]
        public string NewMessageContent { get; set; } = string.Empty;

        [BindProperty] // Added BindProperty for ConversationId to ensure it's always bound from the form
        public int ConversationId { get; set; } // This will be bound from the hidden input

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

            // Ensure ConversationId is set for the hidden input in the form
            this.ConversationId = conversationId; // <--- Make sure this is set in OnGet

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogInformation("OnPostAsync: Form submitted.");
            _logger.LogInformation($"OnPostAsync: ConversationId from form: {ConversationId}"); // Now directly access ConversationId
            _logger.LogInformation($"OnPostAsync: NewMessageContent from form: '{NewMessageContent}'");

            var coachIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (coachIdClaim == null || !int.TryParse(coachIdClaim.Value, out int currentCoachId))
            {
                _logger.LogWarning("OnPostAsync: Coach ID claim not found or invalid during POST. Redirecting to login.");
                return RedirectToPage("/Auth/Login");
            }
            CoachId = currentCoachId;
            _logger.LogInformation($"OnPostAsync: Current CoachId: {CoachId}");

            // IMPORTANT: If ModelState.IsValid is false, you MUST re-populate the 'Conversation' property
            // before returning Page(), otherwise the view will throw NullReferenceException.
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
                // Re-populate Conversation for display when validation fails
                Conversation = await _chatService.GetConversationByIdAsync(this.ConversationId); // Use this.ConversationId from BindProperty
                return Page();
            }
            _logger.LogInformation("OnPostAsync: ModelState is valid.");

            // L?y thông tin cu?c tr? chuy?n hi?n t?i t? database
            var existingConversation = await _chatService.GetConversationByIdAsync(this.ConversationId); // Use this.ConversationId
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

            // Chuy?n hý?ng tr? l?i trang chat ð? hi?n th? tin nh?n m?i
            // This RedirectToPage will trigger a new OnGetAsync, which will correctly populate 'Conversation'
            return RedirectToPage("/Coach/Chat", new { conversationId = existingConversation.ConversationId });
        }
    }
}