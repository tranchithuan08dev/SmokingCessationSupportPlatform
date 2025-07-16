using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.DataAccessObjects.Contexts;
using SmokingCessationSupportPlatform.Repositories;

namespace SmokingCessationSupportPlatform.Services
{
    public class AdminService
    {
        private readonly AdminRepository _adminRepository;
        private readonly SmokingCessationSupportPlatformContext _context;
        public AdminService(AdminRepository adminRepository, SmokingCessationSupportPlatformContext context)
        {
            _adminRepository = adminRepository;
            _context = context;
        }

        public List<User> GetAllUSer()
        {
            return _adminRepository.GetAllUsers();
        }

        public User OnOffStatusUser(int userId)
        {
            var user = _adminRepository.GetUserById(userId);
            if (user != null)
            {
                if (user.IsActive == true)
                {
                    user.IsActive = false;
                }
                else
                {
                    user.IsActive = true;
                }
                _context.SaveChanges();
                return user;
            }
            return null;
        }

        public User PromoteUserToCoach(int userId)
        {
            var user = _adminRepository.GetUserById(userId);
            if (user != null && user.UserRole == "Member")
            {
                user.UserRole = "Coach";
                _context.SaveChanges();
                return user;
            }
            return null;
        }
    }
}
