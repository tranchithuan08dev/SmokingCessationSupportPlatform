using Microsoft.EntityFrameworkCore;
using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.DataAccessObjects;
using SmokingCessationSupportPlatform.DataAccessObjects.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.Repositories
{
   public class CoachRepository :ICoachRepository
    {
        private readonly CoachDAO _coachDAO;
        private readonly SmokingCessationSupportPlatformContext _context;

        public CoachRepository(SmokingCessationSupportPlatformContext context)
        {
            _context = context;
            _coachDAO = new CoachDAO(context);
        }

        public async Task<Coach?> GetCoachByIdAsync(int id)
        {
            return await _coachDAO.GetByIdAsync(id);
        }

        public async Task<Coach?> GetCoachByIdWithUserInfoAsync(int id)
        {
            return await _coachDAO.GetCoachByIdWithUserInfoAsync(id);
        }

        public async Task<List<Coach>> GetAllCoachesAsync()
        {
            return await _coachDAO.GetAllCoachesQuery().ToListAsync();
        }

        public async Task<List<Coach>> GetCoachesByIdsAsync(IEnumerable<int> ids)
        {
            return await _coachDAO.GetCoachesByIdsAsync(ids);
        }

        public async Task AddCoachAsync(Coach coach)
        {
            await _coachDAO.AddAsync(coach);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCoachAsync(Coach coach)
        {
            _coachDAO.Update(coach);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCoachAsync(Coach coach)
        {
            _coachDAO.Delete(coach);
            await _context.SaveChangesAsync();
        }
    }
}
