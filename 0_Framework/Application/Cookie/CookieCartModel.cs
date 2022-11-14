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

        public double PriceWithDiscount { get; set; }
        
    }
}
