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
    public class QuitPlanService : IQuitPlanService
    {
        private readonly IQuitPlanRepository _quitPlanRepository;
        public QuitPlanService(IQuitPlanRepository quitPlanRepository)
        {
            _quitPlanRepository = quitPlanRepository;
        }
        public List<QuitPlan> GetAllQuitPlans()
       =>  _quitPlanRepository.GetAllQuitPlans();


        public List<QuitPlan> GetQuitPlanOfUser(int userId)
       => _quitPlanRepository.GetQuitPlanOfUser(userId);
    }
    
}
