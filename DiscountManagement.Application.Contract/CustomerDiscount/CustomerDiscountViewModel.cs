namespace DiscountManagement.Application.Contract.CustomerDiscount
{
    public class CustomerDiscountViewModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int DiscountRate { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string Reason { get; set; }

        public DateTime StartDateEn { get; set; }

        public DateTime EndDateEn { get; set; }

        public string CreationDate { get; set; }
    }
}
