using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore.Context;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Application.Contracts.Order.UserPanel;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Infrastructure.EFCore.Context;

namespace ShopManagement.Infrastructure.EFCore.Repositories
{
    public class OrderRepository : RepositoryBase<int , Order> , IOrderRepository
    {
        private readonly ShopContext _context;
        private readonly AccountContext _accountContext;

        public OrderRepository(ShopContext context, AccountContext accountContext) : base(context)
        {
            _context = context;
            _accountContext = accountContext;
        }

        //public int UserId(HttpContext httpContext)
        //{
        //    var username = httpContext.User.Identity.Name;

        //    return _accountContext.Accounts.Single(x => x.Username == username)?.Id ?? 0;
        //}

        public double GetAmountBy(int id)
        {
            var order = _context.Orders.Select(x => new { x.PayAmount, x.Id }).FirstOrDefault(x=> x.Id == id);

            if (order != null)
            {
                return order.PayAmount;
            }

            return 0;
        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel)
        {
            var accounts = _accountContext.Accounts.Select(x => new { x.Username, x.Id }).ToList();

            var query = _context.Orders.Select(x => new OrderViewModel
            {
                Id = x.Id,
                AccountId = x.AccountId,
                DiscountAmount = x.DiscountAmount,
                IsCanceled = x.IsCanceled,
                IssueTrackingNo = x.IssueTrackingNo,
                IsPaid = x.IsPaid,
                CreationDate = x.CreationDate.ToFarsi(),
                PayAmount = x.PayAmount,
                PaymentMethodId = x.PaymentMethod,
                TotalAmount = x.TotalAmount,
                RefId = x.RefId
            });

            if (searchModel.AccountId > 0)
            {
                query = query.Where(x => x.AccountId == searchModel.AccountId);
            }

            if (searchModel.IsCanceled)
            {
                query = query.Where(x => x.IsCanceled);
            }

            var orders =  query.OrderByDescending(x => x.Id).ToList();

            foreach (var order in orders)
            {
                order.AccountName = accounts.FirstOrDefault(x => x.Id == order.AccountId).Username;
                order.PaymentMethod = PaymentMethod.GetBy(order.PaymentMethodId).Name;
            }

            return orders;
        }

        public List<OrderItemViewModel> GetItems(int orderId)
        {
            var products = _context.Products.Select(x => new { x.Id, x.Name }).ToList();

            var order = _context.Orders.FirstOrDefault(x => x.Id == orderId);

            if (order == null)
            {
                return new List<OrderItemViewModel>();
            }

            var items = order.Items.Select(x => new OrderItemViewModel
            {
                Id = x.Id,
                Count = x.Count,
                UnitPrice = x.UnitPrice,
                OrderId = x.OrderId,
                ProductId = x.ProductId,
                DiscountAmount = x.DiscountAmount,
                HasDiscount = x.HasDiscount,
                DiscountRate = x.DiscountRate,
                ItemPayAmount = x.ItemPayAmount,
                TotalItemPrice = x.TotalItemPrice,
                UnitPriceWithDiscount = x.UnitPriceWithDiscount
            }).ToList();

            foreach (var item in items)
            {
                item.ProductName = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name;
            }

            return items;
        }

        public List<UserOrdersViewModel> GetUserOrders(int id)
        {
            var query = _context.Orders.Where(x => x.AccountId == id).Select(x => new UserOrdersViewModel
            {
                Id = x.Id,
                IssueTrackingNo = x.IssueTrackingNo,
                //?work on this
                OrderCondition = x.IsPaid,
                RegisterDate = x.CreationDate.ToFarsi(),
                TotalPayAmount = x.PayAmount
            });

            var orders = query.OrderByDescending(x => x.Id).ToList();

            return orders;
        }

        public List<ShowUserOrderViewModel> GetOrderDetails(int orderId)
        {
            var products = _context.Products.Select(x => new { x.Id, x.Name , x.Slug }).ToList();

            var order = _context.Orders.FirstOrDefault(x => x.Id == orderId);

            if (order == null)
            {
                return new List<ShowUserOrderViewModel>();
            }

            var items = order.Items.Select(x => new ShowUserOrderViewModel()
            {
                Id = x.Id,
                Count = x.Count,
                UnitPrice = x.UnitPrice,
                OrderId = x.OrderId,
                ProductId = x.ProductId,
                DiscountAmount = x.DiscountAmount,
                HasDiscount = x.HasDiscount,
                DiscountRate = x.DiscountRate,
                ItemPayAmount = x.ItemPayAmount,
                TotalItemPrice = x.TotalItemPrice,
                UnitPriceWithDiscount = x.UnitPriceWithDiscount,
            }).ToList();


            foreach (var item in items)
            {
                item.ProductName = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name;
                item.ProductSlug = products.FirstOrDefault(x => x.Id == item.ProductId)?.Slug;
            }

            return items;
        }

        public SummaryOrderSectionViewModel GetOrderSummary(int orderId)
        {
            var orders = _context.Orders.Select(x => new SummaryOrderSectionViewModel
            {
                Id = x.Id,
                IssueTrackingNo = x.IssueTrackingNo,
                //?work on this
                OrderCondition = x.IsPaid,
                RegisterDate = x.CreationDate.ToFarsi(),
                TotalPayAmount = x.TotalAmount,
                FinalPayAmount = x.PayAmount,
                PaymentMethodId = x.PaymentMethod,
                TotalDiscountAmount = x.DiscountAmount,
            }).First(x=>x.Id == orderId);

            return orders;
        }
    }
}
