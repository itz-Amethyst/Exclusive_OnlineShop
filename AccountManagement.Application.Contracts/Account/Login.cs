using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Account
{
    public class Login
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string UserName { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Password { get; set; }
    }
}
