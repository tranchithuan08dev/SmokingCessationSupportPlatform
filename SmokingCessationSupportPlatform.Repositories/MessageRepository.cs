using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.DataAccessObjects;
using SmokingCessationSupportPlatform.DataAccessObjects.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.Repositories
{
    public class MessageRepository :IMessageRepository
    {
        private readonly MessageDAO _messageDAO;
        private readonly SmokingCessationSupportPlatformContext _context;

        public MessageRepository(SmokingCessationSupportPlatformContext context)
        {
            _context = context;
            _messageDAO = new MessageDAO(context);
        }

        public async Task<Message?> GetMessageByIdAsync(int id)
        {
            return await _messageDAO.GetByIdAsync(id);
        }

        public async Task<List<Message>> GetConversationMessagesAsync(int conversationId)
        {
            return await _messageDAO.GetConversationMessagesAsync(conversationId);
        }

        public async Task<List<Message>> GetUnreadMessagesInConversationAsync(int conversationId, int receiverId, string isReceiverCoach)
        {
            return await _messageDAO.GetUnreadMessagesInConversationAsync(conversationId, receiverId, isReceiverCoach);
        }

        public async Task AddMessageAsync(Message message)
        {
            await _messageDAO.AddAsync(message);
            await _context.SaveChangesAsync(); 
        }

        public async Task UpdateMessageAsync(Message message) 
        {
            _messageDAO.Update(message);
            await _context.SaveChangesAsync(); 
        }

        public async Task DeleteMessageAsync(Message message) 
        {
            _messageDAO.Delete(message);
            await _context.SaveChangesAsync(); 
        }
    }
}
