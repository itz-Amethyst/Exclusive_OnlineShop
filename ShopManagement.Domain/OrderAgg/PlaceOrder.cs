namespace ShopManagement.Domain.OrderAgg
{
    public class PlaceOrder
    {
        public int AccountId { get; set; }

        public double TotalAmount { get; set; }

        public double DiscountAmount { get; set; }

        public double PayAmount { get; set; }

        public List<AddOrderItem> Items { get; set; }
    }
}
