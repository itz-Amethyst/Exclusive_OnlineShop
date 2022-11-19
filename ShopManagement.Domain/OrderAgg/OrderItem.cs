using _0_Framework.Domain;

namespace ShopManagement.Domain.OrderAgg;

public class OrderItem : EntityBase
{
    public int ProductId { get; private set; }

    public int Count { get; private set; }

    public double UnitPrice { get; private set; }

    public int DiscountRate { get; private set; }

    public int OrderId { get; private set; }

    public Order Order { get; private set; }

    public OrderItem(int productId, int count, double unitPrice, int discountRate)
    {
        ProductId = productId;
        Count = count;
        UnitPrice = unitPrice;
        DiscountRate = discountRate;
    }
}