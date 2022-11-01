using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account.Admin;

namespace AccountManagement.Application.Contracts.Account.User
{
    public class RegisterAccountByUser : RegisterAccount
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string RePassword { get; set; }

    }
}
