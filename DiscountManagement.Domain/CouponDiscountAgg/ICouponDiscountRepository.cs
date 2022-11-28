using _0_Framework.Domain;
using DiscountManagement.Application.Contract.CouponDiscount;

namespace DiscountManagement.Domain.CouponDiscountAgg
{
    public interface ICouponDiscountRepository : IRepository<int , CouponDiscount>
    {
        EditCouponDiscount GetDetails(int id);

        List<CouponDiscountViewModel> Search(CouponDiscountSearchModel searchModel);
    }
}
