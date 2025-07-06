using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.DataAccessObjects.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.DataAccessObjects
{
    public static class UserDAO
    {

        public static void DeleteUser(int userId)
        {
           
            try
            {
                using var db = new SmokingCessationSupportPlatformContext();
                var user = db.Users.FirstOrDefault(u => u.UserId == userId);
                if (user == null)
                {
                    Console.WriteLine($"User with ID {userId} not found.");
                    return;
                }
                user.IsActive = false; 
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting user: {ex.Message}");
            }
        }

        public static void AddUser(User user)
        {
            try
            {
                using var db = new SmokingCessationSupportPlatformContext();
                db.Users.Add(user);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding user: {ex.Message}");

            }
        }

        public static void UpdateUser(User user)
        {
            try
            {
                using var db = new SmokingCessationSupportPlatformContext();
                db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating user: {ex.Message}");

            }
        }

        public static List<User> GetAllUsers()
        {
            try
            {
                using var db = new SmokingCessationSupportPlatformContext();
                var users = db.Users.ToList();
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving users: {ex.Message}");
                return new List<User>();

            }
        }

         public static User GetUserById(int userId)
        {
            try
            {
                using var db = new SmokingCessationSupportPlatformContext();
                var user = db.Users.FirstOrDefault(u => u.UserId == userId);
                if(user == null)
                {
                    Console.WriteLine($"User with ID {userId} not found.");
                    return null;
                }
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving user by ID: {ex.Message}");
                return null;
            }
           
        }

        public static User GetUserByUsername(string username)
        {
            try
            {
                using var db = new SmokingCessationSupportPlatformContext();
                var user = db.Users.FirstOrDefault(u => u.Username == username);
                if (user == null)
                {
                    Console.WriteLine($"User with username {username} not found.");
                    return null;
                }
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving user by username: {ex.Message}");
                return null;
            }
        }

    }
}
