namespace DiscountManagement.Application.Contract.CustomerDiscount
{
    public class CustomerDiscountSearchModel
    {
        public int ProductId { get; private set; }

        public DateTime StartDate { get; private set; }

        public DateTime EndDate { get; private set; }

    }
}
