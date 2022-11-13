using _01_ExclusiveQuery.Contracts.Order;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;

namespace ServiceHost.ViewComponents
{
    public class BasketViewComponent : ViewComponent
    {
        private readonly IOrderQuery _orderQuery;
        public List<CartQueryModel> CartItems;

        [TempData] 
        public string CartMessage { get; set; }
        
        public const string CookieName = "cart-items";

        public BasketViewComponent(IOrderQuery orderQuery)
        {
            _orderQuery = orderQuery;
        }

        public IViewComponentResult Invoke()
        {
            if (Request.Cookies["cart-items"] != null)
            {
                var serializer = new JavaScriptSerializer();
                var value = Request.Cookies[CookieName];
                try
                {
                    CartItems = serializer.Deserialize<List<CartQueryModel>>(value);
                }
                catch (Exception e)
                {
                    ////HttpContext.Response.Cookies.Delete(CookieName);
                    ////var mycookie = HttpContext.Request.Cookies[CookieName];

                    ////Request.Cookies[CookieName] = null;
                    ////var cookieeee = HttpContext.Request.Cookies["cart-items"];

                    ////cookieeee.ex = DateTime.Now.AddDays(-1);

                    //HttpContext.Request.Cookies cookie = new HttpCookie(key)
                    //{
                    //    Expires = DateTime.Now.AddDays(-1) // or any other time in the past
                    //};

                    ////f.Delete(CookieName);

                    ////Console.WriteLine(e);
                    CartMessage = "t";
                    return View(CartItems);
                }



                var basket = _orderQuery.GetCartItemsBy(CartItems);
                return View(basket);

            }

            return View(CartItems);
        }
    }
}
