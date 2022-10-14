using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.User
{
    public class IndexModel : PageModel
    {
        private readonly IAccountApplication _accountApplication;

        //[TempData] 
        //public string Message { get; set; }

        public AccountSearchModel SearchModel;
        public List<AccountViewModel> Accounts;
        public SelectList Roles;

        public IndexModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet(AccountSearchModel searchModel , bool removed = false , bool restored = false)
        {
            ViewData["Removed"] = removed;
            ViewData["Restored"] = restored;
            Accounts = _accountApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateAccount
            {
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateAccount command)
        {
            var account = _accountApplication.Create(command);
            return new JsonResult(account);
        }

        public IActionResult OnGetEdit(int id)
        {
            var account = _accountApplication.GetDetails(id);
            return Partial("Edit", account);
        }

        public JsonResult OnPostEdit(EditAccount command)
        {
            var account = _accountApplication.Edit(command);

            return new JsonResult(account);
        }

        public IActionResult OnGetRemove(int id)
        {
            var result = _accountApplication.Remove(id);

            return RedirectToPage("./Index", new { Removed = "True" });

        }

        public IActionResult OnGetRestore(int id)
        {
            var result = _accountApplication.Restore(id);

            return RedirectToPage("./Index", new { Restored = "True" });
        }
    }
}
