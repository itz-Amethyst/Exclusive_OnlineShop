using _0_Framework.Application;
using _0_Framework.Application.Cookie;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IConfiguration _configuration;
        private readonly IOrderRepository _orderRepository;

        public OrderApplication(IOrderRepository orderRepository, IConfiguration configuration)
        {
            _orderRepository = orderRepository;
            _configuration = configuration;
        }

        public int PlaceOrder(CartModelWithSummary cartModelWithSummary , HttpContext httpContext)
        {
            int userId = _orderRepository.UserId(httpContext);
            //var symbol = _configuration.GetValue<string>("Symbol");
            //var issueTrackingNo = IssueCodeGenerator.Generate(symbol);
            

            var order = new Order(userId , cartModelWithSummary.TotalAmount ,cartModelWithSummary.TotalDiscountAmount , cartModelWithSummary.TotalPayAmount);

            foreach (var cartItem in cartModelWithSummary.Items)
            {
                var orderItem = new OrderItem(cartItem.Id, cartItem.Count, cartItem.UnitPrice, cartItem.DiscountRate);

                order.AddItem(orderItem);
            }

            _orderRepository.Create(order);
            _orderRepository.SaveChanges();

            return order.Id;
        }

        public void PaymentSucceeded(int orderId , int refId)
        {
            var order = _orderRepository.GetById(orderId);

            order.SucceededPayment(refId);

            var symbol = _configuration.GetValue<string>("Symbol");
            var issueTrackingNo = IssueCodeGenerator.Generate(symbol);
            
            order.SetIssueTrackingNo(issueTrackingNo);
            //Todo : Reduce From Inventory
            
            _orderRepository.SaveChanges();
        }
    }
}
