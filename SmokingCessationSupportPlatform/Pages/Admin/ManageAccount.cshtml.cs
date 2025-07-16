using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.Services;

namespace SmokingCessationSupportPlatform.Web.Pages.Admin
{
    public class ManageAccountModel : PageModel
    {
        private readonly AdminService _adminService;

        public ManageAccountModel(AdminService adminService)
        {
            _adminService = adminService;
        }

        public List<User> Users { get; set; }

        public void OnGet()
        {
            Users = _adminService.GetAllUSer();
        }

        public IActionResult OnPostPromote(int userId)
        {
            _adminService.PromoteUserToCoach(userId);
            return RedirectToPage();
        }

        public IActionResult OnPostToggleStatus(int userId)
        {
            _adminService.OnOffStatusUser(userId);
            return RedirectToPage();
        }

    }
}
