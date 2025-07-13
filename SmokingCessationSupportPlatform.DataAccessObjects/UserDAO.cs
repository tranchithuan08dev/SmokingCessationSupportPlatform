using Microsoft.EntityFrameworkCore;
using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.DataAccessObjects.Contexts;

namespace SmokingCessationSupportPlatform.DataAccessObjects
{
    public class UserDAO : BaseDAO<User>
    {
        public UserDAO(SmokingCessationSupportPlatformContext context) : base(context) { }

        public async Task<User?> GetUserByIdWithCoachInfoAsync(int id)
        {
            return await _dbSet
                .Include(u => u.Coach)
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<User>> GetUsersByIdsAsync(IEnumerable<int> ids)
        {
            return await _dbSet.Where(u => ids.Contains(u.UserId)).ToListAsync();
        }

    }
}
