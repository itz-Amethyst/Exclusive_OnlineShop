using _0_Framework.Application;
using _0_Framework.Application.Cookie;

namespace _01_ExclusiveQuery.Contracts.Order
{
    public interface IOrderQuery
    {
        List<CookieCartModel> GetCartItemsBy(List<CookieCartModel> cartItems);

        CartModelWithSummary GetSummary(List<CookieCartModel> cartItems);

        //DiscountUseType ApplyCouponDiscount(List<CookieCartModel> cartItems, string couponCode);
    }
}
