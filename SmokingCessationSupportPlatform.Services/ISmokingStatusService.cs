using SmokingCessationSupportPlatform.BusinessObjects.Models;

namespace SmokingCessationSupportPlatform.Services;

public interface ISmokingStatusService
{
    Task<SmokingStatus?> GetLatestStatusByUserIdAsync(int userId);
    Task<List<SmokingStatus>> GetAllStatusByUserIdAsync(int userId);
    Task<SmokingStatus> AddStatusAsync(SmokingStatus status);
    Task<SmokingStatus> UpdateStatusAsync(SmokingStatus status);
    Task<bool> DeleteStatusAsync(int statusId);
    Task<SmokingStatus?> GetStatusByIdAsync(int statusId);
} 