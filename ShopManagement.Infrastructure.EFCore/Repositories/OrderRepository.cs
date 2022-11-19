using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Infrastructure.EFCore.Context;

namespace ShopManagement.Infrastructure.EFCore.Repositories
{
    public class OrderRepository : RepositoryBase<int , Order> , IOrderRepository
    {
        private readonly ShopContext _context;

        public OrderRepository(DbContext context, ShopContext context1) : base(context)
        {
            _context = context1;
        }
    }
}
