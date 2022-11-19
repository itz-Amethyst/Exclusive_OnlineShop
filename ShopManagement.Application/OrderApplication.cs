using _0_Framework.Application.Cookie;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IOrderRepository _orderRepository;

        public OrderApplication(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public int PlaceOrder(CartModelWithSummary cartModelWithSummary)
        {
            throw new NotImplementedException();
        }
    }
}
