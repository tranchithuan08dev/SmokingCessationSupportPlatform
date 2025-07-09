using Microsoft.EntityFrameworkCore;
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
    public class ConversationRepository : IConservationRepository
    {
        private readonly ConversationDAO _conversationDAO;
        private readonly SmokingCessationSupportPlatformContext _context;

        public ConversationRepository(SmokingCessationSupportPlatformContext context)
        {
            _context = context;
            _conversationDAO = new ConversationDAO(context);
        }

        public async Task<Conversation?> GetConversationByIdAsync(int id)
        {
            return await _conversationDAO.GetByIdAsync(id);
        }

        public async Task<Conversation?> GetConversationByParticipantsAsync(int userId, int coachId)
        {
            return await _conversationDAO.GetConversationByParticipantsAsync(userId, coachId);
        }

        public async Task<List<Conversation>> GetUserConversationsAsync(int userId)
        {
            return await _conversationDAO.GetUserConversationsQuery(userId).ToListAsync();
        }

        public async Task<List<Conversation>> GetCoachConversationsAsync(int coachId)
        {
            return await _conversationDAO.GetCoachConversationsQuery(coachId).ToListAsync();
        }

        public async Task AddConversationAsync(Conversation conversation)
        {
            await _conversationDAO.AddAsync(conversation);
            await _context.SaveChangesAsync(); 
        }

        public async Task UpdateConversationAsync(Conversation conversation) 
        {
            _conversationDAO.Update(conversation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteConversationAsync(Conversation conversation) 
        {
            _conversationDAO.Delete(conversation);
            await _context.SaveChangesAsync(); 
        }
    }
}
