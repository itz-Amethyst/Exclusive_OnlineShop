using _0_Framework.Application.Cookie;

namespace _01_ExclusiveQuery.Contracts.Order
{
    public interface IOrderQuery
    {
        public List<CookieCartModel> GetCartItemsBy(List<CookieCartModel> cartItems);
    }
}
