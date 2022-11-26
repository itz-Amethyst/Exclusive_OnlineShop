namespace ShopManagement.Application.Contracts.Order.UserPanel
{
    public class ShowUserOrderViewModel
    {
        //? order Section

        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int Count { get; set; }

        public double UnitPrice { get; set; }

        public int DiscountRate { get; set; }

        public double TotalItemPrice { get; set; }

        public bool HasDiscount { get; set; }

        public double UnitPriceWithDiscount { get; set; }

        public double DiscountAmount { get; set; }

        public double ItemPayAmount { get; set; }

        public int OrderId { get; set; }
    }
}
