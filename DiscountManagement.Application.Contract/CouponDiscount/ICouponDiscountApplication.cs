using _0_Framework.Application;

namespace DiscountManagement.Application.Contract.CouponDiscount
{
    public interface ICouponDiscountApplication
    {
        OperationResult Define(DefineCouponDiscount command);

        OperationResult Edit(EditCouponDiscount command);

        OperationResult Remove(int id);

        OperationResult Restore(int id);

        EditCouponDiscount GetDetails(int id);

        List<CouponDiscountViewModel> Search(CouponDiscountSearchModel searchModel);
    }
}
