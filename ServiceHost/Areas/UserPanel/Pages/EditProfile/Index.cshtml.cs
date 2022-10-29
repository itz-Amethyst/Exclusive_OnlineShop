using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Account.User;
using AccountManagement.Domain.AccountAgg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.UserPanel.Pages.EditProfile
{
    [Authorize]
    public class IndexModel : PageModel
    {
        [TempData] 
        public string EditMessage { get; set; }

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

        public IActionResult OnPost(EditUserProfile command)
        {
            var result = _accountApplication.EditUserProfile(command);

            if (result.IsSuccessful)
            {
                return RedirectToPage("/EditProfile/Index");
            }

            if (result.IsNeedToLoginAgain)
            {
                return RedirectToPage("/Login");
            }

            EditMessage = result.Message;

            return RedirectToPage("/EditProfile/Index");
        }
    }
}
