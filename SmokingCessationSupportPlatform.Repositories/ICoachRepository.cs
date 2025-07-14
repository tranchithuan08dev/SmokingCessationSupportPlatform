using SmokingCessationSupportPlatform.BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.Repositories
{
    public interface ICoachRepository
    {
        Task<Coach?> GetCoachByIdAsync(int id);
        Task<Coach?> GetCoachByIdWithUserInfoAsync(int id);
        Task<List<Coach>> GetAllCoachesAsync();
        Task<List<Coach>> GetCoachesByIdsAsync(IEnumerable<int> ids);
        Task AddCoachAsync(Coach coach);
        Task UpdateCoachAsync(Coach coach);
        Task DeleteCoachAsync(Coach coach);
    }
}
