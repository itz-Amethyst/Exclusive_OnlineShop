using _0_Framework.Application.Cookie;
using _01_ExclusiveQuery.Contracts.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly ISerializeCookie _serializeCookie;
        private readonly IOrderQuery _orderQuery;

        [TempData] public bool EmptyBasket { get; set; }

        public List<CookieCartModel> CartItems;

        public CartModelWithSummary TotalCartSummaryModel;


        public const string CookieName = "cart-items";
        

        public CheckoutModel(ISerializeCookie serializeCookie, IOrderQuery orderQuery)
        {
            _serializeCookie = serializeCookie;
            _orderQuery = orderQuery;
        }

        public void OnGet()
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
                //    flag = true;
                //    Console.WriteLine(e);
                //}

                //if (flag)
                //{
                //    //CartMessage = "t";
                //    HttpContext.Response.Cookies.Delete(CookieName);
                //    return;
                //}

                if (!_serializeCookie.CheckSerialize(CartItems, HttpContext))
                {
                    //?Just in Case
                    _serializeCookie.DeleteCookie(HttpContext);
                    return;
                }

                CartItems = _serializeCookie.Serialize(CartItems, HttpContext);

                if (CartItems.Count <= 0)
                {
                    EmptyBasket = true;
                    _serializeCookie.DeleteCookie(HttpContext);
                    return;
                }

                EmptyBasket = false;


                CartItems = _orderQuery.GetCartItemsBy(CartItems , HttpContext);

                TotalCartSummaryModel = _orderQuery.GetSummary(CartItems);

            }
        }
    }
}
