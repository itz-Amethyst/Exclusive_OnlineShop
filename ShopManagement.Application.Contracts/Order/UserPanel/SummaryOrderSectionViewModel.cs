namespace ShopManagement.Application.Contracts.Order.UserPanel
{
    public class SummaryOrderSectionViewModel
    {
        public string RegisterDate { get; set; }

        public string IssueTrackingNo { get; set; }

        public double TotalPayAmount { get; set; }

        public double TotalDiscountAmount { get; set; }

        public double FinalPayAmount { get; set; }

        public bool OrderCondition { get; set; }

        public int PaymentMethodId { get; set; }

        public string PaymentMethod { get; set; }
    }
}
