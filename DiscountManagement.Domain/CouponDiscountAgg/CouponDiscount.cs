using _0_Framework.Domain;

namespace DiscountManagement.Domain.CouponDiscountAgg
{
    public class CouponDiscount : EntityBase
    {
        public string DiscountCode { get; private set; }

        public double DiscountPercent { get; private set; }

        public int UsableCount { get; private set; }

        public DateTime StartDate { get; private set; }

        public DateTime EndDate { get; private set; }

        public bool IsDeleted { get; private set; }

        public bool IsOutOfDate { get; private set; }

        public string Reason { get; private set; }

        public DiscountUsage DisUsage { get; private set; }

        public CouponDiscount(string discountCode, double discountPercent, int usableCount, DateTime startDate, DateTime endDate, bool isDeleted, bool isOutOfDate, string reason)
        {
            DiscountCode = discountCode;
            DiscountPercent = discountPercent;
            UsableCount = usableCount;
            StartDate = startDate;
            EndDate = endDate;
            IsDeleted = isDeleted;
            IsOutOfDate = isOutOfDate;
            Reason = reason;
        }

        public void Edit(string discountCode , double discountPercent , int usableCount , DateTime startDate , DateTime endDate , string reason)
        {
            DiscountCode = discountCode;
            DiscountPercent = discountPercent;
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
