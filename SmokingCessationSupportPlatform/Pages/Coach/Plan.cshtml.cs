using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.Services.Interfaces;

namespace SmokingCessationSupportPlatform.Web.Pages.Coach
{
    public class PlanModel : PageModel
    {
        private readonly IUserService _userService;

        public List<User> listUser = new();

        public PlanModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
            listUser = _userService.GetAllUser();
        }
    }
}
