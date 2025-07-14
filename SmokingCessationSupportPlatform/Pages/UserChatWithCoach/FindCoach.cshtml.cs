using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.Repositories;

namespace SmokingCessationSupportPlatform.Web.Pages.UserChatWithCoach
{
    public class FindCoachModel : PageModel
    {
        private readonly ICoachRepository _coachRepository;
        public List<SmokingCessationSupportPlatform.BusinessObjects.Models.Coach> AvailableCoaches { get; set; } = new List<SmokingCessationSupportPlatform.BusinessObjects.Models.Coach>();

        public FindCoachModel(ICoachRepository coachRepository )
        {
            _coachRepository = coachRepository;
        }

        public async Task OnGetAsync()
        {
            AvailableCoaches = await _coachRepository.GetAllCoachesAsync(); 
        }
    }
}
