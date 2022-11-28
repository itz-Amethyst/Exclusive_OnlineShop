using _0_Framework.Application;
using DiscountManagement.Application.Contract.CustomerDiscount;

namespace DiscountManagement.Application.Contract.CouponDiscount
{
    public interface ICouponDiscountApplication
    {
        OperationResult Define(DefineCouponDiscount command);

        OperationResult Edit(EditCouponDiscount command);

        OperationResult Remove(int id);

        OperationResult Restore(int id);

        EditCustomerDiscount GetDetails(int id);

        List<CouponDiscountViewModel> Search(CouponDiscountSearchModel searchModel);
    }
}
