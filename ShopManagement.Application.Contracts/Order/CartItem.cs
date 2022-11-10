namespace ShopManagement.Application.Contracts.Order
{
    public class CartItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double UnitPrice { get; set; }

        public string Picture { get; set; }

        public int Count { get; set; }

        public string Slug { get; set; }

        public double TotalItemPrice { get; set; }

        public CartItem()
        {
            TotalItemPrice = UnitPrice * Count;
        }
    }
}
