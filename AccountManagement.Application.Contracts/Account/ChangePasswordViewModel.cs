using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contracts.Account
{
    public class ChangePasswordViewModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(200, ErrorMessage = " {0} نمیتواند بیشتر از {1} کاراکترباشد")]
        public string Password { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(200, ErrorMessage = " {0} نمیتواند بیشتر از {1} کاراکترباشد")]
        [Compare("Password", ErrorMessage = ValidationMessages.PasswordsDoNotMatch)]
        public string RePassword { get; set; }
    }
}
