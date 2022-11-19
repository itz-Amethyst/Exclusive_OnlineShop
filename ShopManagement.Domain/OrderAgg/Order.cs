﻿using _0_Framework.Domain;

namespace ShopManagement.Domain.OrderAgg
{
    public class Order : EntityBase
    {
        public int AccountId { get; private set; }

        public double TotalAmount { get; private set; }

        public double DiscountAmount { get; private set; }

        public double PayAmount { get; private set; }

        public bool IsPaid { get; private set; }

        public bool IsCanceled { get; private set; }

        public string IssueTrackingNo { get; private set; }

        public int RefId { get; private set; }

        public List<OrderItem> Items { get; private set; }

        public Order(int accountId, double totalAmount, double discountAmount, double payAmount, string issueTrackingNo)
        {
            AccountId = accountId;
            TotalAmount = totalAmount;
            DiscountAmount = discountAmount;
            PayAmount = payAmount;
            IssueTrackingNo = issueTrackingNo;
            Items = new List<OrderItem>();
            IsPaid = false;
            IsCanceled = false;
            RefId = 0;
        }

        public void SucceededPayment(int refId)
        {
            IsPaid = true;
            if (RefId != 0)
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