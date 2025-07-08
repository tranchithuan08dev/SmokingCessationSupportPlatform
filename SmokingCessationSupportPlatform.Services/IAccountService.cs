using SmokingCessationSupportPlatform.BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.Services
{
    public interface IAccountService
    {
        User? LoginByEmailAndPassword(string identifier, string password);
        Task<User?> RegisterUserAsync(string username, string email, string plainPassword, string? fullName = null, DateOnly? dob = null, string? gender = null);
        Task<bool> UserExistsByEmailAsync(string email);
        Task<bool> UserExistsByUsernameAsync(string username);
        string HashPassword(string plainPassword);
        bool VerifyPassword(string hashedPassword, string plainPassword);
        User? GetUserByEmail(string email);
        Task<string?> GenerateAndSavePasswordResetTokenAsync(string email);
        Task<bool> ResetPasswordAsync(string email, string token, string newPassword); 
    }
}
