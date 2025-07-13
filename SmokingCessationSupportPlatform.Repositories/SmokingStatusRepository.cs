using Microsoft.EntityFrameworkCore;
using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.DataAccessObjects.Contexts;

namespace SmokingCessationSupportPlatform.Repositories;

public class SmokingStatusRepository : ISmokingStatusRepository
{
    private readonly SmokingCessationSupportPlatformContext _context;
    public SmokingStatusRepository(SmokingCessationSupportPlatformContext context)
    {
        _context = context;
    }

    public async Task<SmokingStatus?> GetLatestStatusByUserIdAsync(int userId)
    {
        return await _context.SmokingStatuses
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.ReportDate)
            .FirstOrDefaultAsync();
    }

    public async Task<List<SmokingStatus>> GetAllStatusByUserIdAsync(int userId)
    {
        return await _context.SmokingStatuses
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.ReportDate)
            .ToListAsync();
    }

    public async Task<SmokingStatus> AddStatusAsync(SmokingStatus status)
    {
        _context.SmokingStatuses.Add(status);
        await _context.SaveChangesAsync();
        return status;
    }

    public async Task<SmokingStatus> UpdateStatusAsync(SmokingStatus status)
    {
        _context.SmokingStatuses.Update(status);
        await _context.SaveChangesAsync();
        return status;
    }

    public async Task<bool> DeleteStatusAsync(int statusId)
    {
        var status = await _context.SmokingStatuses.FindAsync(statusId);
        if (status == null) return false;
        _context.SmokingStatuses.Remove(status);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<SmokingStatus?> GetStatusByIdAsync(int statusId)
    {
        return await _context.SmokingStatuses.FindAsync(statusId);
    }
} 