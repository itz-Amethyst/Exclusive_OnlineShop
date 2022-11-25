using _0_Framework.Domain;
using ShopManagement.Application.Contracts.Order;

namespace ShopManagement.Domain.OrderAgg
{
    public interface IOrderRepository : IRepository<int , Order>
    {
        //int UserId(HttpContext httpContext);

        double GetAmountBy(int id);

        List<OrderViewModel> Search(OrderSearchModel searchModel);

        List<OrderItemViewModel> GetItems(int orderId);
    }
}
