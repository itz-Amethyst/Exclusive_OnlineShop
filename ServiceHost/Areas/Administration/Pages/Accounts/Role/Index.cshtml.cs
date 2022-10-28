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

        public void OnGet(AccountSearchModel searchModel , bool removed = false , bool restored = false)
        {
            Roles = _roleApplication.List();
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateRole();
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateRole command)
        {
            var account = _roleApplication.Create(command);
            return new JsonResult(account);
        }

        public IActionResult OnGetEdit(int id)
        {
            var role = _roleApplication.GetDetails(id);
            return Partial("Edit", role);
        }

        public JsonResult OnPostEdit(EditRole command)
        {
            var account = _roleApplication.Edit(command);

            return new JsonResult(account);
        }
    }
}
