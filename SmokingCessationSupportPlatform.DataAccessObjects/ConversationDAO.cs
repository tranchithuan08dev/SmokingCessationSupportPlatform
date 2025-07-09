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
    public class ConversationDAO :BaseDAO<Conversation>
    {
        public ConversationDAO(SmokingCessationSupportPlatformContext context) : base(context) { }

        public async Task<Conversation?> GetConversationByParticipantsAsync(int userId, int coachId)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.UserId == userId && c.CoachId == coachId);
        }

        public IQueryable<Conversation> GetUserConversationsQuery(int userId)
        {
            // Bao gồm cả CoachNavigation để lấy thông tin user gốc của coach
            return _dbSet
                .Where(c => c.UserId == userId)
                .Include(c => c.Coach)
                    .ThenInclude(co => co.CoachNavigation); // Tải thông tin User của Coach
        }

        public IQueryable<Conversation> GetCoachConversationsQuery(int coachId)
        {
            // Bao gồm User của conversation
            return _dbSet
                .Where(c => c.CoachId == coachId)
                .Include(c => c.User); // Tải thông tin User liên quan
        }
    }
}
