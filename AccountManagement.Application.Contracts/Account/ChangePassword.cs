using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contracts.Account
{
    public class ChangePassword
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(200, ErrorMessage = " {0} نمیتواند بیشتر از {1} کاراکترباشد")]
        public string Password { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(200, ErrorMessage = " {0} نمیتواند بیشتر از {1} کاراکترباشد")]
        //[Compare("Password", ErrorMessage = ValidationMessages.PasswordsDoNotMatch)]
        public string RePassword { get; set; }
    }
}
