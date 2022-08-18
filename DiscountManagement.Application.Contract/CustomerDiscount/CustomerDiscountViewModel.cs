namespace DiscountManagement.Application.Contract.CustomerDiscount
{
    public class CustomerDiscountViewModel
    {
        public int Id { get; set; }

        public int ProductId { get; private set; }

        public string ProductName { get; set; }

        public int DiscountRate { get; private set; }

        public DateTime StartDate { get; private set; }

        public DateTime EndDate { get; private set; }

        public string Reason { get; private set; }
    }
}
