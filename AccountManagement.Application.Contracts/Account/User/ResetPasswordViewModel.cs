using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contracts.Account.User
{
    public class ResetPasswordViewModel
    {
        public string ActiveCode { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = " {0} نمیتواند بیشتر از {1} کاراکترباشد")]
        public string Password { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = " {0} نمیتواند بیشتر از {1} کاراکترباشد")]
        [Compare(nameof(Password), ErrorMessage = "رمز عبور و تکرار آن یکسان نمیباشد")]
        public string RePassword { get; set; }
    }
}
