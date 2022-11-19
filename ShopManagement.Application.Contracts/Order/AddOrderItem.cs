namespace ShopManagement.Application.Contracts.Order;

public class AddOrderItem
{
    public int ProductId { get; set; }

    public int Count { get; set; }

    public double UnitPrice { get; set; }

    public int DiscountRate { get; set; }
}