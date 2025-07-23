using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.DataAccessObjects.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.DataAccessObjects
{
    public class QuitPlanStagesDAO
    {
        public static void CreateQuitPlanStages(QuitPlanStage quitPlanStage)
        {
            try
            {
                using var context = new SmokingCessationSupportPlatformContext();
                context.QuitPlanStages.Add(quitPlanStage);
                context.SaveChanges();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<QuitPlanStage> GetAllQuitPlanStages()
        {
            try
            {
                using var context = new SmokingCessationSupportPlatformContext();
                return context.QuitPlanStages.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
