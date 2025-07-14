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
    public class UserRepository : IUserRepository
    {
        private readonly UserDAO _userDAO;
        private readonly SmokingCessationSupportPlatformContext _context;

        public UserRepository(SmokingCessationSupportPlatformContext context)
        {
            _context = context;
            _userDAO = new UserDAO(context);
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _userDAO.GetByIdAsync(id);
        }

        public async Task<User?> GetUserByIdWithCoachInfoAsync(int id)
        {
            return await _userDAO.GetUserByIdWithCoachInfoAsync(id);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userDAO.GetUserByEmailAsync(email);
        }

        public async Task<List<User>> GetUsersByIdsAsync(IEnumerable<int> ids)
        {
            return await _userDAO.GetUsersByIdsAsync(ids);
        }

        public async Task AddUserAsync(User user)
        {
            await _userDAO.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _userDAO.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User user)
        {
            _userDAO.Delete(user);
            await _context.SaveChangesAsync();
        }

        public List<User> GetAllUsers()
        => UserDAO.GetAllUserMember();
    }
}
