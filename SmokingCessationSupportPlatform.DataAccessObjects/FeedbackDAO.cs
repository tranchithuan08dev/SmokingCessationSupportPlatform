using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.DataAccessObjects.Contexts;
using Microsoft.EntityFrameworkCore;

namespace SmokingCessationSupportPlatform.DataAccessObjects
{
    public class FeedbackDAO
    {
        public static List<Feedback> GetAllFeedbacks()
        {
            try
            {
                using var context = new SmokingCessationSupportPlatformContext();
                return context.Feedbacks
                              .Include(f => f.User)
                              .OrderByDescending(f => f.FeedbackDate)
                              .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving feedbacks", ex);
            }
        }
        public static void CreateFeedback(Feedback feedback)
        {
            try
            {
                using var context = new SmokingCessationSupportPlatformContext();
                context.Feedbacks.Add(feedback);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lưu phản hồi", ex);
            }
        }

    }
}
