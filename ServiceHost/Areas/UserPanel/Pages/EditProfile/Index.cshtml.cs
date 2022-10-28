using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Account.User;
using AccountManagement.Domain.AccountAgg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.UserPanel.Pages.EditProfile
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAccountApplication _accountApplication;
        private readonly IAccountRepository _accountRepository;

        public EditUserProfile Command;

        public IndexModel(IAccountApplication accountApplication, IAccountRepository accountRepository)
        {
            _accountApplication = accountApplication;
            _accountRepository = accountRepository;
        }

        public void OnGet()
        {
            Command = _accountRepository.GetProfileDetails(User.Identity.Name);
        }
    }
}
