using _0_Framework.Application.Cookie;
using ShopManagement.Application.Contracts.Order.UserPanel;

namespace ShopManagement.Application.Contracts.Order
{
    public interface IOrderApplication
    {
        int PlaceOrder(CartModelWithSummary cartModelWithSummary);

        string PaymentSucceeded(int orderId , int refId);

        double GetAmountBy(int id);

        List<OrderViewModel> Search(OrderSearchModel searchModel);

        void Cancel(int id);

        List<OrderItemViewModel> GetItems(int orderId);

        List<UserOrdersViewModel> GetUserOrders(int id);

        List<ShowUserOrderViewModel> GetOrderDetails(int orderId);
    }
}
 