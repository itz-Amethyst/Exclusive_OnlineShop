using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AccountManagement.Application.Contracts.Account
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = " {0} نمیتواند بیشتر از {1} کاراکترباشد")]
        [EmailAddress(ErrorMessage = "ایمل وارد شده معتبر نمیباشد")]
        public string Email { get; set; }
    }
}
