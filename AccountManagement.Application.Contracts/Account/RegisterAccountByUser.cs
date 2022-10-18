using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Account
{
    public class RegisterAccountByUser : RegisterAccount
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string RePassword { get; set; }

    }
}
