using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ForgotPasswordModel : PageModel
    {
        [TempData]
        public string ForgotMessage { get; set; }

        private readonly IAccountApplication _accountApplication;

        public ForgotPasswordViewModel Command;

        public ForgotPasswordModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }


        public void OnGet()
        {
        }

        public IActionResult OnPost(ForgotPasswordViewModel command)
        {
            var result = _accountApplication.SendForgotPasswordLink(command.Email);

            if (result.IsSuccessful)
            {
                ForgotMessage = result.Message;
                return RedirectToPage("/ForgotPassword");
            }

            ForgotMessage = result.Message;

            return RedirectToPage("/ForgotPassword");
        }
    }
}
