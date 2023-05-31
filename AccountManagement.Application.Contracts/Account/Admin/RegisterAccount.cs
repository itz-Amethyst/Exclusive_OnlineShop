using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Http;

namespace AccountManagement.Application.Contracts.Account.Admin
{
    public class RegisterAccount
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(200, ErrorMessage = " {0} نمیتواند بیشتر از {1} کاراکترباشد")]
        public string Username { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(200, ErrorMessage = " {0} نمیتواند بیشتر از {1} کاراکترباشد")]
        public string Password { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(200, ErrorMessage = " {0} نمیتواند بیشتر از {1} کاراکترباشد")]
        [EmailAddress(ErrorMessage = ValidationMessages.EmailIsNotValid)]
        public string Email { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(11, ErrorMessage = " {0} نمیتواند بیشتر از {1} کاراکترباشد")]
        public string Mobile { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        public int RoleId { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        //[MaxLength(200, ErrorMessage = " {0} نمیتواند بیشتر از {1} کاراکترباشد")]
        public IFormFile ProfilePhoto { get; set; }

        public List<RoleViewModel> Roles { get; set; }
    }
}
