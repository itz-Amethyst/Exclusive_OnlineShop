using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ServiceHost.Pages
{
    public class LoginModel : PageModel
    {
        [TempData]
        public string LoginMessage { get; set; }
        [TempData] 
        public string UserNameValidation { get; set; }
        [TempData]
        public string PasswordValidation { get; set; }


        private readonly IAccountApplication _accountApplication;

        public LoginModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost(Login command)
        {
            if (string.IsNullOrWhiteSpace(command.UserNameOrEmail))
            {
                UserNameValidation = "نام کاربری را وارد کنید";
                return RedirectToPage("/Login");
            }

            if (string.IsNullOrWhiteSpace(command.Password))
            {
                PasswordValidation = "رمز عبور را وارد کنید";
                return RedirectToPage("/Login");
            }

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
