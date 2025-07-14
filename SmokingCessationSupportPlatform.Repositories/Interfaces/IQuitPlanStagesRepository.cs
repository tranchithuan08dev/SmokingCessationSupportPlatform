using SmokingCessationSupportPlatform.BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.Repositories.Interfaces
{
    public interface IQuitPlanStagesRepository
    {
        void CreateQuitPlanStages(QuitPlanStage quitPlanStage);

        List<QuitPlanStage> GetAllQuitPlanStages();
    }
}
