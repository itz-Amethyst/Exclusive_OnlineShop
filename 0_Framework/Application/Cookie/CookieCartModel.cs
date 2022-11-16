namespace _0_Framework.Application.Cookie
{
    public class CookieCartModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public string Picture { get; set; }

        public int Count { get; set; }

        public double UnitPrice { get; set; }

        public double TotalItemPrice { get; set; }

        public bool HasDiscount { get; set; }

        public int DiscountRate { get; set; }

        public double UnitPriceWithDiscount { get; set; }

        public bool IsInStock { get; set; }

        public double DiscountAmount { get; set; }
        
        public double ItemPayAmount { get; set; }

    }

    public class CartModelWithSummary
    {
        //? summary
        public double TotalAmount { get; set; }
        public double TotalDiscountAmount { get; set; }
        public double TotalPayAmount { get; set; }
        public int PaymentMethod { get; set; }
        public List<CookieCartModel> Items { get; set; }

        public CartModelWithSummary()
        {
            Items = new List<CookieCartModel>();
        }

        public void Add(CookieCartModel cartItem)
        {
            Items.Add(cartItem);
            TotalAmount += cartItem.TotalItemPrice;
            TotalDiscountAmount += cartItem.DiscountAmount;
            TotalPayAmount += cartItem.ItemPayAmount;
        }

        public void SetPaymentMethod(int methodId)
        {
            PaymentMethod = methodId;
        }
    }
}
