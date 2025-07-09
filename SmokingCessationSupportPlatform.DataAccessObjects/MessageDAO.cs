using Microsoft.EntityFrameworkCore;
using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.DataAccessObjects.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.DataAccessObjects
{
        public class MessageDAO : BaseDAO<Message>
    {
        public MessageDAO(SmokingCessationSupportPlatformContext context) : base(context) { }

        public async Task<List<Message>> GetConversationMessagesAsync(int conversationId)
        {
            return await _dbSet
                .Where(m => m.ConversationId == conversationId)
                .OrderBy(m => m.SentAt)
                .ToListAsync();
        }

        public async Task<List<Message>> GetUnreadMessagesInConversationAsync(int conversationId, int receiverId, string receiverType)
        {

            return await _dbSet
                .Where(m => m.ConversationId == conversationId &&
                            m.ToId == receiverId &&
                            m.ToType == receiverType &&
                            !m.IsRead &&
                            (m.FromId != receiverId || m.FromType != receiverType)
                            )
                .ToListAsync();
        }
    }
}
