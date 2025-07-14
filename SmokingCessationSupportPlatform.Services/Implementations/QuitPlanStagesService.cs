using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.Repositories.Interfaces;
using SmokingCessationSupportPlatform.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.Services.Implementations
{
    public class QuitPlanStagesService : IQuitPlanStagesService
    {
        private readonly IQuitPlanStagesRepository _quitPlanStagesRepository;

        public QuitPlanStagesService(IQuitPlanStagesRepository quitPlanStagesRepository)
        {
            _quitPlanStagesRepository = quitPlanStagesRepository;
        }
        public void CreateQuitPlanStages(QuitPlanStage quitPlanStage)
        => _quitPlanStagesRepository.CreateQuitPlanStages(quitPlanStage);

        public List<QuitPlanStage> GetAllQuitPlanStages()
        => _quitPlanStagesRepository.GetAllQuitPlanStages();
    }
}
