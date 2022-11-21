using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore.Context;
using Microsoft.AspNetCore.Http;
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

        public int UserId(HttpContext httpContext)
        {
            var username = httpContext.User.Identity.Name;

            return _accountContext.Accounts.Single(x => x.Username == username)?.Id ?? 0;
        }

        public double GetAmountBy(int id)
        {
            var order = _context.Orders.Select(x => new { x.PayAmount, x.Id }).FirstOrDefault(x=> x.Id == id);

            if (order != null)
            {
                return order.PayAmount;
            }

            return 0;
        }
    }
}
