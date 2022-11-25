using _0_Framework.Application;
using _0_Framework.Application.Cookie;
using Microsoft.Extensions.Configuration;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IConfiguration _configuration;
        private readonly IOrderRepository _orderRepository;
        private readonly IShopInventoryAcl _shopInventoryAcl;
        private readonly IAuthHelper _authHelper;

        public OrderApplication(IOrderRepository orderRepository, IConfiguration configuration, IShopInventoryAcl shopInventoryAcl, IAuthHelper authHelper)
        {
            _orderRepository = orderRepository;
            _configuration = configuration;
            _shopInventoryAcl = shopInventoryAcl;
            _authHelper = authHelper;
        }

        public int PlaceOrder(CartModelWithSummary cartModelWithSummary)
        {
            int userId = _authHelper.CurrentAccountId();
            //var symbol = _configuration.GetValue<string>("Symbol");
            //var issueTrackingNo = IssueCodeGenerator.Generate(symbol);
            

            var order = new Order(userId , cartModelWithSummary.PaymentMethod , cartModelWithSummary.TotalAmount ,cartModelWithSummary.TotalDiscountAmount , cartModelWithSummary.TotalPayAmount);

            foreach (var cartItem in cartModelWithSummary.Items)
            {
                var orderItem = new OrderItem(cartItem.Id, cartItem.Count, cartItem.UnitPrice, cartItem.DiscountRate, cartItem.TotalItemPrice, cartItem.HasDiscount, cartItem.UnitPriceWithDiscount, cartItem.DiscountAmount, cartItem.ItemPayAmount);

                order.AddItem(orderItem);
            }

            _orderRepository.Create(order);
            _orderRepository.SaveChanges();

            return order.Id;
        }

        public string PaymentSucceeded(int orderId , int refId)
        {
            var order = _orderRepository.GetById(orderId);

            //var orrrrder = _shop.Orders.Find(orderId);
            order.SucceededPayment(refId);

            var symbol = _configuration.GetValue<string>("Symbol");
            var issueTrackingNo = IssueCodeGenerator.Generate(symbol);

            order.SetIssueTrackingNo(issueTrackingNo);
            //Todo : Reduce From Inventory

            if (_shopInventoryAcl.ReduceFromInventory(order.Items))
            {
                _orderRepository.SaveChanges();
                return issueTrackingNo;
            }

            return "";
        }

        public double GetAmountBy(int id)
        {
            return _orderRepository.GetAmountBy(id);
        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel)
        {
            return _orderRepository.Search(searchModel);
        }

        public void Cancel(int id)
        {
            var order = _orderRepository.GetById(id);

            order.Cancel();

            _orderRepository.SaveChanges();;
        }

        public List<OrderItemViewModel> GetItems(int orderId)
        {
            return _orderRepository.GetItems(orderId);
        }
    }
}
