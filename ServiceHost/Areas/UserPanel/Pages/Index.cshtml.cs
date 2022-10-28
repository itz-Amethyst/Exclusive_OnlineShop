using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Account.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.UserPanel.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IAccountApplication _accountApplication;

        public AccountInformationViewModel Information;

        public IndexModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet()
        {
            Information = _accountApplication.GetUserInformation(User.Identity.Name);
        }
    }
}
