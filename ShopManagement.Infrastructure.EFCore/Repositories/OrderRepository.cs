using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Infrastructure.EFCore.Context;

namespace ShopManagement.Infrastructure.EFCore.Repositories
{
    public class OrderRepository : RepositoryBase<int , Order> , IOrderRepository
    {
        private readonly ShopContext _context;
        private readonly AccountContext _accountContext;

        public OrderRepository(DbContext context, ShopContext context1, AccountContext accountContext) : base(context)
        {
            _context = context1;
            _accountContext = accountContext;
        }

        public int UserId(HttpContext httpContext)
        {
            var username = httpContext.User.Identity.Name;

            return _accountContext.Accounts.Single(x => x.Username == username)?.Id ?? 0;
        }
    }
}
