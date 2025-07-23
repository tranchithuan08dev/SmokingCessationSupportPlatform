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
    public class QuitPlanDAO
    {
        public static List<QuitPlan> GetAllQuitPlans()
        {
            using (var context = new SmokingCessationSupportPlatformContext())
            {
                return context.QuitPlans.ToList();
            }
        }

        public static List<QuitPlan> GetQuitPlanOfUser(int userId)
        {
            var listQuitPlanOfUser = new List<QuitPlan>();
            try
            {
                using var context = new SmokingCessationSupportPlatformContext();
                listQuitPlanOfUser = context.QuitPlans.Include(q => q.QuitPlanStages).Where(u => u.UserId.Equals(userId)).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return listQuitPlanOfUser;
        }
    }
}
