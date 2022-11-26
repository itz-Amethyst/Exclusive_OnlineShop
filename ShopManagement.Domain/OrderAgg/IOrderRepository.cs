using _0_Framework.Domain;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Application.Contracts.Order.UserPanel;

namespace ShopManagement.Domain.OrderAgg
{
    public interface IOrderRepository : IRepository<int , Order>
    {
        //int UserId(HttpContext httpContext);

        double GetAmountBy(int id);

        List<OrderViewModel> Search(OrderSearchModel searchModel);

        List<OrderItemViewModel> GetItems(int orderId);

        //? UserPanel

        List<UserOrdersViewModel> GetUserOrders(int id);

        List<ShowUserOrderViewModel> GetOrderDetails(int orderId);
    }
}
