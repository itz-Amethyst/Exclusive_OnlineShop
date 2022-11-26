namespace ShopManagement.Application.Contracts.Order.UserPanel
{
    public class ShowUserOrderViewModel
    {
        public string RegisterDate { get; set; }

        public string IssueTrackingNo { get; set; }

        public double TotalPayAmount { get; set; }

        public double TotalDiscountAmount { get; set; }

        public double FinalPayAmount { get; set; }

        public bool OrderCondition { get; set; }

        public int PaymentMethodId { get; set; }

        public string PaymentMethod { get; set; }

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
