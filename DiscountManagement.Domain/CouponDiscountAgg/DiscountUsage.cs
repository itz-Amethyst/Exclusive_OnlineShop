using _0_Framework.Domain;

namespace DiscountManagement.Domain.CouponDiscountAgg
{
    public class DiscountUsage : EntityBase
    {
        public int UserId { get; set; }

        public int DiscountId { get; set; }

        //public int OrderId { get; set; }

        public CouponDiscount CouponDiscount { get; private set; }

        public DiscountUsage(int userId, int discountId)
        {
            UserId = userId;
            DiscountId = discountId;
        }

        public DiscountUsage()
        {
        }
    }
}
