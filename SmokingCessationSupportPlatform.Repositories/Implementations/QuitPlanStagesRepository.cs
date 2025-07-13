using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.DataAccessObjects;
using SmokingCessationSupportPlatform.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.Repositories.Implementations
{
    public class QuitPlanStagesRepository : IQuitPlanStagesRepository
    {
        public void CreateQuitPlanStages(QuitPlanStage quitPlanStage)
        => QuitPlanStagesDAO.CreateQuitPlanStages(quitPlanStage);

        public List<QuitPlanStage> GetAllQuitPlanStages()
        => QuitPlanStagesDAO.GetAllQuitPlanStages();
    }
}
