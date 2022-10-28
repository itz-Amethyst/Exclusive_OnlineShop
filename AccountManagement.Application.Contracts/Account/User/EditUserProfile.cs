using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace AccountManagement.Application.Contracts.Account.User
{
    public class EditUserProfile
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(200, ErrorMessage = " {0} نمیتواند بیشتر از {1} کاراکترباشد")]
        public string UserName { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(11, ErrorMessage = " {0} نمیتواند بیشتر از {1} کاراکترباشد")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(200, ErrorMessage = " {0} نمیتواند بیشتر از {1} کاراکترباشد")]
        [EmailAddress(ErrorMessage = ValidationMessages.EmailIsNotValid)]
        public string Email { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public IFormFile ProfilePicture { get; set; }

        public string Image { get; set; }
    }
}
