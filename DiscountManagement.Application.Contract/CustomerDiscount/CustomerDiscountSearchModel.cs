namespace DiscountManagement.Application.Contract.CustomerDiscount
{
    public class CustomerDiscountSearchModel
    {
        public int ProductId { get; private set; }

        public string StartDate { get; private set; }

        public string EndDate { get; private set; }

    }
}
