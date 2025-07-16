using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.DataAccessObjects.Contexts;

namespace SmokingCessationSupportPlatform.Repositories
{
    public class AdminRepository
    {
        private readonly SmokingCessationSupportPlatformContext _context;
        public AdminRepository(SmokingCessationSupportPlatformContext context)
        {
            _context = context;
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.Where(u => u.UserRole != "Admin").ToList();
        }

        public User GetUserById(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user != null)
            {
                return user;
            }
            return null;
        }
    }
}
