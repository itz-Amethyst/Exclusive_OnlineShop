using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;

namespace DiscountManagement.Application.Contract.ColleagueDiscount
{
    public class DefineColleagueDiscount
    {
        [Range(1,100000 , ErrorMessage = ValidationMessages.IsRequired)]
        public int ProductId { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Range(1,99 , ErrorMessage = ValidationMessages.NotValid)]
        public int DiscountRate { get; set; }

        public List<ProductViewModel> Products { get; set; }
    }
}
