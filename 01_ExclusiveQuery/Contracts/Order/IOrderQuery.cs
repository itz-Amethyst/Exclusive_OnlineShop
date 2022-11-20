using _0_Framework.Application.Cookie;
using Microsoft.AspNetCore.Http;

namespace _01_ExclusiveQuery.Contracts.Order
{
    public interface IOrderQuery
    {
        List<CookieCartModel> GetCartItemsBy(List<CookieCartModel> cartItems , HttpContext httpContext);

        CartModelWithSummary GetSummary(List<CookieCartModel> cartItems , HttpContext httpContext);
    }
}
