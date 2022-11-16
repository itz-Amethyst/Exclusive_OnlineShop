using _0_Framework.Application.Cookie;
using _01_ExclusiveQuery.Contracts.Order;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class BasketViewComponent : ViewComponent
    {
        private readonly IOrderQuery _orderQuery;
        private readonly ISerializeCookie _serializeCookie;
        public List<CookieCartModel> CartItems;

        public const string CookieName = "cart-items";

        public BasketViewComponent(IOrderQuery orderQuery, ISerializeCookie serializeCookie)
        {
            _orderQuery = orderQuery;
            _serializeCookie = serializeCookie;
        }

        public IViewComponentResult Invoke()
        {
            if (Request.Cookies["cart-items"] != null)
            {
                //var serializer = new JavaScriptSerializer();
                //var value = Request.Cookies[CookieName];
                //try
                //{
                //    CartItems = serializer.Deserialize<List<CartQueryModel>>(value);
                //}
                //catch (Exception e)
                //{
                //    ////HttpContext.Response.Cookies.Delete(CookieName);
                //    ////var mycookie = HttpContext.Request.Cookies[CookieName];

                //    ////Request.Cookies[CookieName] = null;
                //    ////var cookieeee = HttpContext.Request.Cookies["cart-items"];

                //    ////cookieeee.ex = DateTime.Now.AddDays(-1);

                //    //HttpContext.Request.Cookies cookie = new HttpCookie(key)
                //    //{
                //    //    Expires = DateTime.Now.AddDays(-1) // or any other time in the past
                //    //};

                //    ////f.Delete(CookieName);

                //    ////Console.WriteLine(e);
                //    Console.WriteLine(e);
                //    HttpContext.Response.Cookies.Delete(CookieName);
                //    return View(CartItems);
                //}

                if (!_serializeCookie.CheckSerialize(CartItems, HttpContext))
                {
                    //?Just in Case
                    HttpContext.Response.Cookies.Delete(CookieName);
                    return View(CartItems);
                }

                CartItems = _serializeCookie.Serialize(CartItems, HttpContext);

                if (CartItems.Count <= 0)
                {
                    _serializeCookie.DeleteCookie(HttpContext);
                    return View(CartItems);
                }

                var basket = _orderQuery.GetCartItemsBy(CartItems, HttpContext);
                return View(basket);

            }

            return View(CartItems);
        }
    }
}
