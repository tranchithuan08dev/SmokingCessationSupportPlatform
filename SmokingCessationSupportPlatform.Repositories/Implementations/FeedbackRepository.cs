using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.DataAccessObjects;
using SmokingCessationSupportPlatform.Repositories.Interfaces;

namespace SmokingCessationSupportPlatform.Repositories.Implementations
{
    public class FeedbackRepository : IFeedbackRepository
    {
        public List<Feedback> GetAllFeedbacks()
            => FeedbackDAO.GetAllFeedbacks();
        public void CreateFeedback(Feedback feedback)
        {
            FeedbackDAO.CreateFeedback(feedback);
        }

    }
}
