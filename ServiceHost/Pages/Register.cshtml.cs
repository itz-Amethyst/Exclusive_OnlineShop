using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IAccountApplication _accountApplication;

        [TempData]
        public string RegisterMessage { get; set; }
        [TempData]
        public string RegisterValidation { get; set; }

        public RegisterModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost(RegisterAccountByUser command)
        {
            if (string.IsNullOrWhiteSpace(command.Fullname))
            {
                RegisterValidation = "نام را وارد کنید";
                return RedirectToPage("/Register");
            }

            if (string.IsNullOrWhiteSpace(command.Password))
            {
                RegisterValidation = "رمز عبور را وارد کنید";
                return RedirectToPage("/Register");
            }

            if (string.IsNullOrWhiteSpace(command.Username))
            {
                RegisterValidation = "نام کاربری را وارد  کنید";
                return RedirectToPage("/Register");
            }

            if (string.IsNullOrWhiteSpace(command.Mobile))
            {
                RegisterValidation = "شماره موبایل وارد کنید";
                return RedirectToPage("/Register");
            }

            if (command.Password != command.RePassword)
            {
                RegisterMessage = "پسورد وارد شده با تکرار آن مطابقت ندارد لطفا دوباره تلاش کنید";
                return RedirectToPage("/Register");
            }

            var result = _accountApplication.Register(command);

            if (result.IsSuccessful)
            {
                return RedirectToPage("/Index");
            }

            RegisterMessage = result.Message;

            return RedirectToPage("/Register");


        }
    }
}
