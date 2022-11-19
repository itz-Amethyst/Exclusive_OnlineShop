using _0_Framework.Domain;

namespace ShopManagement.Domain.OrderAgg
{
    public class Order : EntityBase
    {
        public int AccountId { get; private set; }

        public double TotalAmount { get; private set; }

        public int DiscountRate { get; private set; }

        public double DiscountAmount { get; private set; }

        public double PayAmount { get; private set; }

        public bool IsPayed { get; private set; }

        public bool IsCanceled { get; private set; }

        public string IssueTrackingNo { get; private set; }

        public int RefId { get; private set; }

        public List<OrderItem> Items { get; private set; }

        public Order(int accountId, double totalAmount, double discountAmount, double payAmount, string issueTrackingNo, List<OrderItem> items , int discountRate)
        {
            AccountId = accountId;
            TotalAmount = totalAmount;
            DiscountAmount = discountAmount;
            DiscountRate = discountRate;
            PayAmount = payAmount;
            IssueTrackingNo = issueTrackingNo;
            Items = items;
            IsPayed = false;
            IsCanceled = false;
            RefId = 0;
        }
    }

    public class OrderItem : EntityBase
    {

    }
}
