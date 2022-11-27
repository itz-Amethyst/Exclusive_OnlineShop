using _0_Framework.Domain;

namespace DiscountManagement.Domain.CouponDiscountAgg
{
    public class DiscountUsage : EntityBase
    {
        public int UserId { get; private set; }

        public int DiscountId { get; private set; }

        public int OrderId { get; private set; }

        public CouponDiscount CouponDiscount { get; private set; }

        public DiscountUsage(int userId, int discountId, int orderId)
        {
            UserId = userId;
            DiscountId = discountId;
            OrderId = orderId;
        }
    }
}
