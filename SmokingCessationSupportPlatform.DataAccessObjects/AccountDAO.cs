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
    public class AccountDAO
    {
        private readonly SmokingCessationSupportPlatformContext _dbContext;

        public AccountDAO(SmokingCessationSupportPlatformContext dbContext)
        {
            _dbContext = dbContext;
        }


        public User? GetUserByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
        }

        public User? GetUserByUsername(string username)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Username.ToLower() == username.ToLower());
        }

        public async Task<User?> AddUserAsync(User newUser)
        {
            if (newUser == null)
            {
                throw new ArgumentNullException(nameof(newUser), "User object cannot be null.");
            }

            newUser.UserRole ??= "Member";
            newUser.IsActive ??= true;
            newUser.RegistrationDate ??= DateTime.UtcNow; 

            _dbContext.Users.Add(newUser);
            var savedChanges = await _dbContext.SaveChangesAsync(); 

            return savedChanges > 0 ? newUser : null; 
        }
        public async Task<bool> UserExistsByEmailAsync(string email)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<bool> UserExistsByUsernameAsync(string username)
        {
            return await _dbContext.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower());
        }

        public async Task AddPasswordResetTokenAsync(PasswordResetToken token)
        {
            await _dbContext.PasswordResetTokens.AddAsync(token);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PasswordResetToken?> GetValidPasswordResetTokenAsync(string email, string tokenValue)
        {
            return await _dbContext.PasswordResetTokens
                .Include(prt => prt.User)
                .FirstOrDefaultAsync(prt =>
                    prt.User != null &&
                    prt.User.Email.ToLower() == email.ToLower() &&
                    prt.Token == tokenValue &&
                    prt.ExpiryTime > DateTime.UtcNow &&
                    prt.IsUsed == false);
        }

        public async Task UpdatePasswordResetTokenAsync(PasswordResetToken token)
        {
            _dbContext.PasswordResetTokens.Update(token);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }


    }
}
