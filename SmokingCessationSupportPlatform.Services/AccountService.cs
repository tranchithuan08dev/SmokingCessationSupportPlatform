using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.Repositories;
using System;
using BCrypt;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public string HashPassword(string plainPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(plainPassword);
        }

        public User? GetUserByEmail(string email)
        {
            return _accountRepository.GetUserByEmail(email);
        }

        public User LoginByEmailAndPassword(string identifier, string password)
        {
            if (string.IsNullOrWhiteSpace(identifier) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Email and password cannot be null or empty.");

            var user = _accountRepository.GetUserByEmail(identifier);
            if (user == null)
            {
                user = _accountRepository.GetUserByUsername(identifier); 
            }

            if (user == null)
            {
                return null; 
            }

            if (VerifyPassword(user.PasswordHash, password))
            {
                return user;
            }

            return null; 
        }

        public async Task<User?> RegisterUserAsync(string username, string email, string plainPassword, string? fullName = null, DateOnly? dob = null, string? gender = null)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(plainPassword))
            {
                throw new ArgumentException("Username, Email, and Password are required for registration.");
            }

            if (await _accountRepository.UserExistsByUsernameAsync(username))
            {
                throw new ArgumentException("Username already exists.");
            }
            if (await _accountRepository.UserExistsByEmailAsync(email))
            {
                throw new ArgumentException("Email already exists.");
            }

            var passwordHash = HashPassword(plainPassword);

            var newUser = new User
            {
                Username = username,
                Email = email,
                PasswordHash = passwordHash, 
                FullName = fullName,
                DateOfBirth = dob,
                Gender = gender,
                RegistrationDate = DateTime.UtcNow, 
                LastLoginDate = null,
            };

            return await _accountRepository.AddUserAsync(newUser);
        }

        public async Task<bool> UserExistsByEmailAsync(string email)
        {
            return await _accountRepository.UserExistsByEmailAsync(email);
        }

        public async Task<bool> UserExistsByUsernameAsync(string username)
        {
            return await _accountRepository.UserExistsByUsernameAsync(username);
        }

        public bool VerifyPassword(string hashedPassword, string plainPassword)
        {
            return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
        }
        public async Task<string?> GenerateAndSavePasswordResetTokenAsync(string email)
        {
            var user = _accountRepository.GetUserByEmail(email);
            if (user == null)
            {
                return null; 
            }

            var tokenValue = Guid.NewGuid().ToString("N");
            var expiryTime = DateTime.UtcNow.AddHours(1);

            var passwordResetToken = new PasswordResetToken
            {
                UserId = user.UserId,
                Token = tokenValue,
                ExpiryTime = expiryTime
            };

            await _accountRepository.AddPasswordResetTokenAsync(passwordResetToken);

            return tokenValue;
        }
        public async Task<bool> ResetPasswordAsync(string email, string tokenValue, string newPassword)
        {
            var passwordResetToken = await _accountRepository.GetValidPasswordResetTokenAsync(email, tokenValue);

            if (passwordResetToken == null || passwordResetToken.User == null)
            {
                return false;
            }

            if (passwordResetToken.User.Email.ToLower() != email.ToLower())
            {
                return false; 
            }

            var newPasswordHash = HashPassword(newPassword);

            await _accountRepository.UpdateUserPasswordAsync(passwordResetToken.User, newPasswordHash);

            await _accountRepository.InvalidatePasswordResetTokenAsync(passwordResetToken);

            return true;
        }
    }
}
