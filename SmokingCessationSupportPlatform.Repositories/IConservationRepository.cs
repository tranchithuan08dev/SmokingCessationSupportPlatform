using SmokingCessationSupportPlatform.BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.Repositories
{
    public interface IConservationRepository
    {
        Task<Conversation?> GetConversationByIdAsync(int id);
        Task<Conversation?> GetConversationByParticipantsAsync(int userId, int coachId);
        Task<List<Conversation>> GetUserConversationsAsync(int userId);
        Task<List<Conversation>> GetCoachConversationsAsync(int coachId);
        Task AddConversationAsync(Conversation conversation);
        Task UpdateConversationAsync(Conversation conversation); 
        Task DeleteConversationAsync(Conversation conversation);
        Task<Conversation?> GetConversationByIdWithMessagesAsync(int id); 

    }
}
