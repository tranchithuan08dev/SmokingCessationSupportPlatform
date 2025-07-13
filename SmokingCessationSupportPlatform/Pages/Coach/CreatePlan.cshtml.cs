using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SmokingCessationSupportPlatform.Web.Pages.Coach
{
    public class CreatePlanModel : PageModel
    {
        private readonly IQuitPlanService _quitPlanService;
        private readonly IQuitPlanStagesService _quitPlanStagesService;
        public CreatePlanModel(IQuitPlanService quitPlanService, IQuitPlanStagesService quitPlanStagesService
            )
        {
            _quitPlanService = quitPlanService;
            _quitPlanStagesService = quitPlanStagesService;
        }

        public List<QuitPlan> QuitPlans = new List<QuitPlan>();
        public List<QuitPlan> AvailablePlans = new List<QuitPlan>();

        [BindProperty]
        public QuitPlan NewQuitPlan { get; set; }
        [BindProperty]
        public QuitPlanStage StageInput { get; set; }

        [BindProperty(SupportsGet = true)]
        public int UserID { get; set; }
        public void OnGet()
        {
            QuitPlans = _quitPlanService.GetQuitPlanOfUser(UserID);
            AvailablePlans = _quitPlanService.GetQuitPlanOfUser(UserID);
        }


        public async Task<IActionResult> OnPostAssignStagesAsync()
        {
            

            var stage = new QuitPlanStage
            {
                PlanId = StageInput.PlanId,
                StageName = StageInput.StageName,
                Description = StageInput.Description,
                TargetDate = StageInput.TargetDate,
                IsCompleted = true
            };

             _quitPlanStagesService.CreateQuitPlanStages(stage); 

            return RedirectToPage("/Coach/Plan"); 
        }

    }


}

