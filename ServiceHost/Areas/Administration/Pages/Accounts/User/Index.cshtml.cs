using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Account.Admin;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Infrastructure.EFCore.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.User
{
    [Authorize]
    [PermissionChecker(_0_Framework.Infrastructure.Roles.ManageUsers)]
    public class IndexModel : PageModel
    {
        private readonly IAccountApplication _accountApplication;
        private readonly IRoleApplication _roleApplication;

        //[TempData] 
        //public string Message { get; set; }

        public AccountSearchModel SearchModel;
        public List<AccountViewModel> Accounts;
        public SelectList Roles;

        public IndexModel(IAccountApplication accountApplication, IRoleApplication roleApplication)
        {
            _accountApplication = accountApplication;
            _roleApplication = roleApplication;
        }

        public void OnGet(AccountSearchModel searchModel , bool removed = false , bool restored = false)
        {
            ViewData["Removed"] = removed;
            ViewData["Restored"] = restored;
            Roles = new SelectList(_roleApplication.List(), "Id", "Name");
            Accounts = _accountApplication.Search(searchModel);
        }

        [PermissionChecker(_0_Framework.Infrastructure.Roles.CreateUser)]
        public IActionResult OnGetCreate()
        {
            var command = new RegisterAccount
            {
                Roles = _roleApplication.List()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(RegisterAccount command)
        {
            var account = _accountApplication.Create(command);
            return new JsonResult(account);
        }

        [PermissionChecker(_0_Framework.Infrastructure.Roles.EditUser)]
        public IActionResult OnGetEdit(int id)
        {
            var account = _accountApplication.GetDetails(id);
            account.Roles = _roleApplication.List();
            return Partial("Edit", account);
        }

        public JsonResult OnPostEdit(EditAccount command)
        {
            var account = _accountApplication.Edit(command);

            return new JsonResult(account);
        }

        [PermissionChecker(_0_Framework.Infrastructure.Roles.DeleteUser)]
        public IActionResult OnGetRemove(int id)
        {
            var result = _accountApplication.Remove(id);

            return RedirectToPage("./Index", new { Removed = "True" });

        }

        [PermissionChecker(_0_Framework.Infrastructure.Roles.DeleteUser)]
        public IActionResult OnGetRestore(int id)
        {
            var result = _accountApplication.Restore(id);

            return RedirectToPage("./Index", new { Restored = "True" });
        }

        [PermissionChecker(_0_Framework.Infrastructure.Roles.ChangeUserPassword)]
        public IActionResult OnGetChangePassword(int id)
        {
            var command = new ChangePasswordViewModel { Id = id };
            return Partial("ChangePassword", command);
        }

        public JsonResult OnPostChangePassword(ChangePasswordViewModel command)
        {
            var result = _accountApplication.ChangePassword(command);

            return new JsonResult(result);
        }
    }
}
