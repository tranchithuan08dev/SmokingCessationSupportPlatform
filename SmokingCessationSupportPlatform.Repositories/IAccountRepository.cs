using SmokingCessationSupportPlatform.BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.Repositories
{
    public interface IAccountRepository
    {
        Task<User?> AddUserAsync(User newUser);
        Task<bool> UserExistsByEmailAsync(string email);
        Task<bool> UserExistsByUsernameAsync(string username);
        User? GetUserByEmail(string email);
        User? GetUserByUsername(string username);
        Task AddPasswordResetTokenAsync(PasswordResetToken token);
        Task<PasswordResetToken?> GetValidPasswordResetTokenAsync(string email, string token);
        Task InvalidatePasswordResetTokenAsync(PasswordResetToken token); 
        Task UpdateUserPasswordAsync(User user, string newPasswordHash);
    }
}
