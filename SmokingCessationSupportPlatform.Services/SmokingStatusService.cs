using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.Repositories;

namespace SmokingCessationSupportPlatform.Services;

public class SmokingStatusService : ISmokingStatusService
{
    private readonly ISmokingStatusRepository _repo;
    public SmokingStatusService(ISmokingStatusRepository repo)
    {
        _repo = repo;
    }
    public async Task<SmokingStatus?> GetLatestStatusByUserIdAsync(int userId)
        => await _repo.GetLatestStatusByUserIdAsync(userId);
    public async Task<List<SmokingStatus>> GetAllStatusByUserIdAsync(int userId)
        => await _repo.GetAllStatusByUserIdAsync(userId);
    public async Task<SmokingStatus> AddStatusAsync(SmokingStatus status)
        => await _repo.AddStatusAsync(status);
    public async Task<SmokingStatus> UpdateStatusAsync(SmokingStatus status)
        => await _repo.UpdateStatusAsync(status);
    public async Task<bool> DeleteStatusAsync(int statusId)
        => await _repo.DeleteStatusAsync(statusId);
    public async Task<SmokingStatus?> GetStatusByIdAsync(int statusId)
        => await _repo.GetStatusByIdAsync(statusId);
} 