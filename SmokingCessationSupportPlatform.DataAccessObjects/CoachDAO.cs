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
    public class CoachDAO : BaseDAO<Coach>
    {
        public CoachDAO(SmokingCessationSupportPlatformContext context) : base(context) { }

        public async Task<Coach?> GetCoachByIdWithUserInfoAsync(int id)
        {
            return await _dbSet
                .Include(c => c.CoachNavigation) 
                .FirstOrDefaultAsync(c => c.CoachId == id);
        }

        public async Task<List<Coach>> GetCoachesByIdsAsync(IEnumerable<int> ids)
        {
            return await _dbSet
                .Include(c => c.CoachNavigation) 
                .Where(c => ids.Contains(c.CoachId))
                .ToListAsync();
        }

        public IQueryable<Coach> GetAllCoachesQuery()
        {
            return _dbSet.Include(c => c.CoachNavigation);
        }
    }
}
