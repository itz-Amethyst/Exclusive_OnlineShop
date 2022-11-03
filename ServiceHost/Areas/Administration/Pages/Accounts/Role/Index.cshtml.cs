using AccountManagement.Application.Contracts.Account.Admin;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    [Authorize(Roles = _0_Framework.Infrastructure.Roles.Administrator)]
    public class IndexModel : PageModel
    {
        private readonly IRoleApplication _roleApplication;

        //[TempData] 
        //public string Message { get; set; }

        public List<RoleViewModel> Roles;

        public IndexModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        public void OnGet(bool created = false, bool edited = false ,bool activated = false , bool deactivated = false)
        {
            ViewData["Created"] = created;
            ViewData["Edited"] = edited;
            ViewData["Activated"] = activated;
            ViewData["DeActivated"] = deactivated;
            Roles = _roleApplication.List();
        }

        public IActionResult OnGetDeActive(int id)
        {
            var result = _roleApplication.Remove(id);

            return RedirectToPage("./Index", new { DeActivated = "True" });

        }

        public IActionResult OnGetActive(int id)
        {
            var result = _roleApplication.Restore(id);

            return RedirectToPage("./Index", new { Activated = "True" });
        }
    }
}
