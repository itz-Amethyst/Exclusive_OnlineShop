using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace DiscountManagement.Application.Contract.CouponDiscount
{
    public class DefineCouponDiscount
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string CouponCode { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Range(1, 99, ErrorMessage = ValidationMessages.NotValid)]
        public int DiscountRate { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Range(1, int.MaxValue, ErrorMessage = ValidationMessages.NotValid)]
        public int UsableCount { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string StartDate { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string EndDate { get; set; }

        public string Reason { get; set; }

    }
}
