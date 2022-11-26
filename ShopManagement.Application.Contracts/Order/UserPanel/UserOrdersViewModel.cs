namespace ShopManagement.Application.Contracts.Order.UserPanel
{
    public class UserOrdersViewModel
    {
        public int Id { get; set; }

        public string RegisterDate { get; set; }

        public string IssueTrackingNo { get; set; }

        public double TotalPayAmount { get; set; }

        public bool OrderCondition { get; set; }
    }
}
