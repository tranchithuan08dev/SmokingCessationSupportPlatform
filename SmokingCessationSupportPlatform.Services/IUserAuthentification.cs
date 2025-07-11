using SmokingCessationSupportPlatform.BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.Services
{
    public interface IUserAuthentification
    {
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByIdWithCoachInfoAsync(int id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<List<User>> GetUsersByIdsAsync(IEnumerable<int> ids);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);
    }
}
