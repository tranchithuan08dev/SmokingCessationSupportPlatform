using Microsoft.EntityFrameworkCore;
using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.DataAccessObjects.Contexts;
using SmokingCessationSupportPlatform.Repositories;

namespace SmokingCessationSupportPlatform.Services
{
    public class UserService
    {
        IUserRepository _IUserRepo;
        SmokingCessationSupportPlatformContext _context;
        public UserService(IUserRepository repo, SmokingCessationSupportPlatformContext context)
        {
            _IUserRepo = repo;
            _context = context;
        }

        public async Task<User?> GetUserWithProgressAsync(int UserId)
        {
            return await _context.Users
                .Include(u => u.QuitProgresses)
                .Include(u => u.UserAchievements)
                    .ThenInclude(u => u.Achievement)
                .FirstOrDefaultAsync(u => u.UserId == UserId);
        }
    }
}
