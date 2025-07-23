using Microsoft.EntityFrameworkCore;
using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.DataAccessObjects.Contexts;

namespace SmokingCessationSupportPlatform.Services
{
    public class AchievementService
    {
        private readonly SmokingCessationSupportPlatformContext _context;

        public AchievementService(SmokingCessationSupportPlatformContext context)
        {
            _context = context;
        }

        public async Task<Achievement?> GetByIdAsync(int id)
        {
            return await _context.Achievements.FindAsync(id);
        }

        public async Task UpdateAchievementAsync(Achievement achievement)
        {
            var existing = await _context.Achievements.FindAsync(achievement.AchievementId);
            if (existing == null) return;

            existing.AchievementName = achievement.AchievementName;
            existing.Description = achievement.Description;
            existing.Criteria = achievement.Criteria;
            existing.IconUrl = achievement.IconUrl;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAchievementAsync(int id)
        {
            var achievement = await _context.Achievements.FindAsync(id);
            if (achievement != null)
            {
                _context.Achievements.Remove(achievement);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Achievement>> GetAllAsync()
        {
            return await _context.Achievements.ToListAsync();
        }

        public async Task CreateAsync(Achievement achievement)
        {
            _context.Achievements.Add(achievement);
            await _context.SaveChangesAsync();
        }
    }
}
