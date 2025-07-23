using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.DataAccessObjects.Contexts;

namespace SmokingCessationSupportPlatform.Repositories
{
    public class AchievementRepository
    {
        private readonly SmokingCessationSupportPlatformContext _context;

        public AchievementRepository(SmokingCessationSupportPlatformContext context)
        {
            _context = context;
        }

        public List<Achievement> GetAll()
        {
            return _context.Achievements.ToList();
        }

        public Achievement? GetById(int id)
        {
            return _context.Achievements.FirstOrDefault(a => a.AchievementId == id);
        }

        public void Add(Achievement achievement)
        {
            _context.Achievements.Add(achievement);
            _context.SaveChanges();
        }

        public void Update(Achievement achievement)
        {
            _context.Achievements.Update(achievement);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var a = _context.Achievements.FirstOrDefault(x => x.AchievementId == id);
            if (a != null)
            {
                _context.Achievements.Remove(a);
                _context.SaveChanges();
            }
        }
    }
}
