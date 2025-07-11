using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountDAO _accountDAO;

        public AccountRepository(AccountDAO accountDAO)
        {
            _accountDAO = accountDAO;
        }

        public async Task<User?> AddUserAsync(User newUser)
        {
            return await _accountDAO.AddUserAsync(newUser);
        }

        public User? GetUserByEmail(string email)
        {
            return _accountDAO.GetUserByEmail(email);
        }

        public User? GetUserByUsername(string username)
        {
            return _accountDAO.GetUserByUsername(username);
        }

        public async Task<bool> UserExistsByEmailAsync(string email)
        {
            return await _accountDAO.UserExistsByEmailAsync(email);
        }

        public async Task<bool> UserExistsByUsernameAsync(string username)
        {
            return await _accountDAO.UserExistsByUsernameAsync(username);
        }

        public async Task AddPasswordResetTokenAsync(PasswordResetToken token)
        {
            await _accountDAO.AddPasswordResetTokenAsync(token);
        }

        public async Task<PasswordResetToken?> GetValidPasswordResetTokenAsync(string email, string tokenValue)
        {
            return await _accountDAO.GetValidPasswordResetTokenAsync(email, tokenValue);
        }

        public async Task InvalidatePasswordResetTokenAsync(PasswordResetToken token)
        {
            token.IsUsed = true;
            await _accountDAO.UpdatePasswordResetTokenAsync(token);
        }

        public async Task UpdateUserPasswordAsync(User user, string newPasswordHash)
        {
            user.PasswordHash = newPasswordHash;
            await _accountDAO.UpdateUserAsync(user);
        }

        public User? GetUserById(int id)
        {
            return _accountDAO.GetUserById(id);
        }
    }
}
