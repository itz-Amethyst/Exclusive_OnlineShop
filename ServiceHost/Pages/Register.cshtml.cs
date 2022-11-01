using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Account.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IAccountApplication _accountApplication;

        public RegisterAccountByUser Command;

        [TempData]
        public string RegisterMessage { get; set; }

        public RegisterModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost(RegisterAccountByUser command)
        {
            var result = _accountApplication.Register(command);

            if (result.IsSuccessful)
            {
                return Partial("_SuccessRegister", command);

            }

            RegisterMessage = result.Message;

            return RedirectToPage("/Register");
        }
    }
}
