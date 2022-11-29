using _0_Framework.Domain;

namespace DiscountManagement.Domain.CouponDiscountAgg
{
    public class CouponDiscount : EntityBase
    {
        public string DiscountCode { get; private set; }

        public int DiscountRate { get; private set; }

        public int UsableCount { get; set; }

        public DateTime StartDate { get; private set; }

        public DateTime EndDate { get; private set; }

        public bool IsDeleted { get; private set; }

        public bool IsOutOfDate { get; set; }

        public string Reason { get; private set; }

        public DiscountUsage DisUsage { get; private set; }

        public CouponDiscount(string discountCode, int discountRate, int usableCount, DateTime startDate, DateTime endDate , string reason)
        {
            DiscountCode = discountCode;
            DiscountRate = discountRate;
            UsableCount = usableCount;
            StartDate = startDate;
            EndDate = endDate;
            IsDeleted = false;
            IsOutOfDate = false;
            Reason = reason;
        }

        public void Edit(string discountCode , int discountRate , int usableCount , DateTime startDate , DateTime endDate , string reason)
        {
            DiscountCode = discountCode;
            DiscountRate = discountRate;
            UsableCount = usableCount;
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;
        }

        public void Remove()
        {
            IsDeleted = true;
        }

        public void Restore()
        {
            IsDeleted = false;
        }
    }
}
