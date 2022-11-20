using _0_Framework.Application.Cookie;
using _01_ExclusiveQuery.Contracts.Order;
using _01_ExclusiveQuery.Contracts.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages
{
    [Authorize]
    public class CheckoutModel : PageModel
    {
        private readonly ISerializeCookie _serializeCookie;
        private readonly IOrderQuery _orderQuery;
        private readonly IProductQuery _productQuery;
        private readonly IOrderApplication _orderApplication;

        [TempData] public bool EmptyBasket { get; set; }

        public List<CookieCartModel> CartItems;

        public CartModelWithSummary TotalCartSummaryModel;


        public const string CookieName = "cart-items";
        

        public CheckoutModel(ISerializeCookie serializeCookie, IOrderQuery orderQuery, IProductQuery productQuery, IOrderApplication orderApplication)
        {
            _serializeCookie = serializeCookie;
            _orderQuery = orderQuery;
            _productQuery = productQuery;
            _orderApplication = orderApplication;
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

        public IActionResult OnGetPay()
        {
            if (Request.Cookies["cart-items"] != null)
            {
                if (!_serializeCookie.CheckSerialize(CartItems, HttpContext))
                {
                    //?Just in Case
                    _serializeCookie.DeleteCookie(HttpContext);
                    return RedirectToPage("/Index");
                }

                CartItems = _serializeCookie.Serialize(CartItems, HttpContext);

                if (CartItems.Count <= 0)
                {
                    EmptyBasket = true;
                    _serializeCookie.DeleteCookie(HttpContext);
                    return RedirectToPage("/Index");
                }

                EmptyBasket = false;


                CartItems = _orderQuery.GetCartItemsBy(CartItems, HttpContext);

                TotalCartSummaryModel = _orderQuery.GetSummary(CartItems);

                var result = _productQuery.CheckInventoryStatus(TotalCartSummaryModel.Items); //CartItems Mishe

                if (result.Any(x => !x.IsInStock))
                {
                    return RedirectToPage("/Cart");
                }

                var orderId = _orderApplication.PlaceOrder(TotalCartSummaryModel , HttpContext);
            }

            return null;
        }
    }
}
