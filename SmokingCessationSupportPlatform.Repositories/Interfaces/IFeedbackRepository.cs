using SmokingCessationSupportPlatform.BusinessObjects.Models;

namespace SmokingCessationSupportPlatform.Repositories.Interfaces
{
    public interface IFeedbackRepository
    {
        List<Feedback> GetAllFeedbacks();
        void CreateFeedback(Feedback feedback);
    }
}
