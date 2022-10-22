using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ResetPasswordModel : PageModel
    {
        [TempData]
        public string ResetMessage { get; set; }

        [TempData]
        public string ResetSuccessMessage { get; set; }


        private readonly IAccountApplication _accountApplication;

        public ResetPasswordViewModel Command;

        public ResetPasswordModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public ActionResult OnGet(string id)
        {
            ViewData["ActiveCode"] = id;

            return Page();
        }

        public IActionResult OnPost(ResetPasswordViewModel command)
        {
            var result = _accountApplication.ResetPassword(command);

            if (result.IsSuccessful)
            {
                ResetSuccessMessage = result.Message;
                return RedirectToPage("/ResetPassword");
            }

            ResetMessage = result.Message;

            return RedirectToPage("/ResetPassword");
        }
    }
}
