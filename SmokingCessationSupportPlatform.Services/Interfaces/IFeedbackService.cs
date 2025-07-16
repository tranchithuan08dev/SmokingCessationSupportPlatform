using SmokingCessationSupportPlatform.BusinessObjects.Models;

namespace SmokingCessationSupportPlatform.Services.Interfaces
{
    public interface IFeedbackService
    {
        List<Feedback> GetAllFeedbacks();
        void CreateFeedback(Feedback feedback);
    }
}
