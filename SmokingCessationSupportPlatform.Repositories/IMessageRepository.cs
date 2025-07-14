using SmokingCessationSupportPlatform.BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.Repositories
{
    public interface IMessageRepository
    {
        Task<Message?> GetMessageByIdAsync(int id);
        Task<List<Message>> GetConversationMessagesAsync(int conversationId);
        Task<List<Message>> GetUnreadMessagesInConversationAsync(int conversationId, int receiverId, string receiverType);
        Task AddMessageAsync(Message message);
        Task UpdateMessageAsync(Message message); 
        Task DeleteMessageAsync(Message message);
    }
}
