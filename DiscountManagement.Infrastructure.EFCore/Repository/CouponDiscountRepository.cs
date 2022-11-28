using _0_Framework.Infrastructure;
using DiscountManagement.Domain.CouponDiscountAgg;
using DiscountManagement.Infrastructure.EFCore.Context;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class CouponDiscountRepository : RepositoryBase<int , CouponDiscount> , ICouponDiscountRepository
    {
        private readonly DiscountContext _context;
        public CouponDiscountRepository(DiscountContext context) : base(context)
        {
            _context = context;
        }
    }
}
