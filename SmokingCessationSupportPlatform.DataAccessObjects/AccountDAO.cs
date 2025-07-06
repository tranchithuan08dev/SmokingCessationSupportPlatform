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
        public static User LoginByEmailOrUsername(string identifier, string password)
        {
            try
            {
                using var db = new SmokingCessationSupportPlatformContext();

                var user = db.Users
                    .FirstOrDefault(u =>
                        (u.Email == identifier || u.Username == identifier) &&
                        u.PasswordHash == password && 
                        u.IsActive == true);

                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging in user: {ex.Message}");
                return null;
            }
        }

        public static void Register(string username, string email, string plainPassword, string? fullName = null, DateOnly? dob = null, string? gender = null)
        {
            using var db = new SmokingCessationSupportPlatformContext();

            if (db.Users.Any(u => u.Username == username || u.Email == email))
                throw new Exception("Username or Email already exists.");

            var passwordHash = HashPassword(plainPassword);

            var user = new User
            {
                Username = username,
                Email = email,
                PasswordHash = passwordHash,
                FullName = fullName,
                DateOfBirth = dob,
                Gender = gender,
                RegistrationDate = DateTime.Now,
            };

            db.Users.Add(user);
            db.SaveChanges();
        }

       

        private static string HashPassword(string password)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

    }
}
