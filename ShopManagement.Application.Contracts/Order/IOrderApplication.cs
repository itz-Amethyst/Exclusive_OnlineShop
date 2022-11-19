using _0_Framework.Application.Cookie;

namespace ShopManagement.Application.Contracts.Order
{
    public interface IOrderApplication
    {
        int PlaceOrder(CartModelWithSummary cartModelWithSummary);
    }
}
