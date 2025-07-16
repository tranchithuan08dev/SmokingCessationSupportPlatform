using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.Repositories.Interfaces;
using SmokingCessationSupportPlatform.Services.Interfaces;

namespace SmokingCessationSupportPlatform.Services.Implementations
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public List<Feedback> GetAllFeedbacks()
            => _feedbackRepository.GetAllFeedbacks();
        public void CreateFeedback(Feedback feedback)
        {
            _feedbackRepository.CreateFeedback(feedback);
        }

    }
}
