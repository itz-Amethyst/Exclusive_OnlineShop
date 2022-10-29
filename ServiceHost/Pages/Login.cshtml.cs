using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Account.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class LoginModel : PageModel
    {
        [TempData]
        public string LoginMessage { get; set; }

        private readonly IAccountApplication _accountApplication;

        public Login Command;

        public LoginModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet()
        {
            ViewData["ChangePassword"] = false;
        }

        public IActionResult OnPost(Login command)
        {
            var result = _accountApplication.Login(command);

            if (result.IsSuccessful)
            {
                return RedirectToPage("/Index");
            }

            LoginMessage = result.Message;

            return RedirectToPage("/Login");

        }

        public IActionResult OnGetLogout()
        {
            _accountApplication.Logout();

            return RedirectToPage("/Index");
        }

        public IActionResult OnGetLogoutFromAdmin()
        {
            return RedirectToPage("/Index");
        }
    }
}
