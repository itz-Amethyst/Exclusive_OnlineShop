using _0_Framework.Domain;

namespace ShopManagement.Domain.OrderAgg;

public class OrderItem : EntityBase
{
    public int ProductId { get; private set; }

    public int Count { get; private set; }

    public double UnitPrice { get; private set; }

    public int DiscountRate { get; private set; }

    public double TotalItemPrice { get; private set; }

    public bool HasDiscount { get; private set; }

    public double UnitPriceWithDiscount { get; private set; }

    public double DiscountAmount { get; private set; }

    public double ItemPayAmount { get; private set; }

    public int OrderId { get; private set; }

    public Order Order { get; private set; }

    public OrderItem(int productId, int count, double unitPrice, int discountRate, double totalItemPrice, bool hasDiscount, double unitPriceWithDiscount, double discountAmount, double itemPayAmount)
    {
        ProductId = productId;
        Count = count;
        UnitPrice = unitPrice;
        DiscountRate = discountRate;
        TotalItemPrice = totalItemPrice;
        HasDiscount = hasDiscount;
        UnitPriceWithDiscount = unitPriceWithDiscount;
        DiscountAmount = discountAmount;
        ItemPayAmount = itemPayAmount;
    }
}