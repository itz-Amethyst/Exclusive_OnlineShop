using _0_Framework.Domain;

namespace ShopManagement.Domain.OrderAgg
{
    public class Order : EntityBase
    {
        public int AccountId { get; private set; }

        public int PaymentMethod { get; private set; }

        public double TotalAmount { get; private set; }

        public double DiscountAmount { get; private set; }

        public double PayAmount { get; private set; }

        public bool IsPaid { get; private set; }

        public bool IsCanceled { get; private set; }

        //? Just need to add this fucking question to make it null and shut the mouth of sql
        public string? IssueTrackingNo { get; private set; }

        public int RefId { get; private set; }

        public List<OrderItem> Items { get; private set; }

        public Order(int accountId,int paymentMethod, double totalAmount, double discountAmount, double payAmount)
        {
            AccountId = accountId;
            PaymentMethod = paymentMethod;
            TotalAmount = totalAmount;
            DiscountAmount = discountAmount;
            PayAmount = payAmount;
            IsPaid = false;
            IsCanceled = false;
            RefId = 0;
            Items = new List<OrderItem>();
        }

        public void SucceededPayment(int refId)
        {
            IsPaid = true;
            if (refId != 0)
            {
                RefId = refId;
            }
        }

        public void SetIssueTrackingNo(string number)
        {
            IssueTrackingNo = number;
        }

        public void Cancel()
        {
            IsCanceled = true;
        }

        public void AddItem(OrderItem item)
        {
            Items.Add(item);
        }
    }
}
