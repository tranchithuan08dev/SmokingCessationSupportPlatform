using SmokingCessationSupportPlatform.BusinessObjects.DTO;
using SmokingCessationSupportPlatform.BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.Services
{
    public interface IChatService
    {
        Task<Conversation> GetOrCreateConversationAsync(int userId, int coachId);
        Task<List<Conversation>> GetUserConversationsAsync(int userId);
        Task<List<Conversation>> GetCoachConversationsAsync(int coachId);
        Task<MessageViewModel?> SendMessageAsync(int conversationId, int fromId, string fromType, int toId, string toType, string content);
        Task<List<MessageViewModel>> GetConversationMessagesWithSenderInfoAsync(int conversationId);
        Task MarkMessagesAsReadAsync(int conversationId, int receiverId, string receiverType);
        Task<Conversation?> GetConversationByIdAsync(int conversationId); 
    }
}
