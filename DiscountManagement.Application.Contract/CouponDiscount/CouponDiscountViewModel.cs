namespace DiscountManagement.Application.Contract.CouponDiscount
{
    public class CouponDiscountViewModel
    {
        public int Id { get; set; }

        public string CouponCode { get; set; }

        public int DiscountRate { get; set; }

        public int UsableCount { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string Reason { get; set; }

        public DateTime StartDateEn { get; set; }

        public DateTime EndDateEn { get; set; }

        public string CreationDate { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsOutOfDate { get; set; }
    }
}
