using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.UserPanel.Pages.ChangePassword
{
    
    public class IndexModel : PageModel
    {
        [TempData]
        public string ChangePasswordMessage { get; set; }

        private readonly IAccountApplication _accountApplication;

        public ChangePasswordViewModel Command;

        public IndexModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public IActionResult OnPost(ChangePasswordViewModel command)
        {
            var result = _accountApplication.ChangePasswordByUser(command);

            if (result.IsSuccessful)
            {
                return Redirect("/Login?ChangePassword=true");
            }

            ChangePasswordMessage = result.Message;

            return RedirectToPage("/ChangePassword/Index");
        }
    }
}
