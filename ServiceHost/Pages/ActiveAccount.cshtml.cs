using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ActiveAccountModel : PageModel
    {
        private readonly IAccountApplication _accountApplication;

        public ActiveAccountModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        [TempData]
        public bool ActiveMessage { get; set; }

        public PageResult OnGet(string id)
        {

            ActiveMessage = _accountApplication.ActiveAccount(id);

            return Page();
        }
    }
}
