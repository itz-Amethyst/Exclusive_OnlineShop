using _0_Framework.Application.Cookie;
using _01_ExclusiveQuery.Contracts.Order;
using _01_ExclusiveQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class CartModel : PageModel
    {
        private readonly ISerializeCookie _serializeCookie;
        private readonly IOrderQuery _orderQuery;
        private readonly IProductQuery _productQuery;

        [TempData] public bool EmptyBasket { get; set; }

        public List<CookieCartModel> CartItems;
        public const string CookieName = "cart-items";

        public CartModel(IOrderQuery orderQuery, ISerializeCookie serializeCookie, IProductQuery productQuery)
        {
            _orderQuery = orderQuery;
            _serializeCookie = serializeCookie;
            _productQuery = productQuery;
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

                CartItems = _serializeCookie.DeSerialize(CartItems, HttpContext);

                if (CartItems.Count <= 0)
                {
                    EmptyBasket = true;
                    _serializeCookie.DeleteCookie(HttpContext);
                    return;
                }

                EmptyBasket = false;


                CartItems = _productQuery.CheckInventoryStatus(CartItems);

                CartItems = _orderQuery.GetCartItemsBy(CartItems);

            }

            //Response.Cookies.Append("test" , serializer , cookieOptions);

            //? Only check first product work on this
            //List<Productv> product = _productRepository.GetById(CartItems[0].Id);

            //if (_productRepository.Exists(x => product == CartItems[0]))
            //{

            //}
        }

        public IActionResult OnGetRemoveFromCart(int id)
        {
            //var serializer = new JavaScriptSerializer();
            //var value = Request.Cookies[CookieName];
            //////_serializeCookie.DeleteCookie(HttpContext);
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
            //    HttpContext.Response.Cookies.Delete(CookieName);
            //    return RedirectToPage("./Cart");
            //}

            //if (!_serializeCookie.Serialize(CartItems, HttpContext))
            //{
            //    //!Just in Case
            //    _serializeCookie.DeleteCookie(HttpContext);
            //    return RedirectToPage("./Cart");
            //}


            _serializeCookie.RemoveItemFromCookie(CartItems , id , HttpContext);

            return RedirectToPage("/Cart");
        }

        public IActionResult OnPostGoToCheckOutPage()
        {
            //Work on this

            if (Request.Cookies["cart-items"] != null)
            {
                if (!_serializeCookie.CheckSerialize(CartItems, HttpContext))
                {
                    //?Just in Case
                    _serializeCookie.DeleteCookie(HttpContext);
                    return RedirectToPage("/Cart");
                }

                CartItems = _serializeCookie.DeSerialize(CartItems, HttpContext);

                if (CartItems.Count <= 0)
                {
                    EmptyBasket = true;
                    _serializeCookie.DeleteCookie(HttpContext);
                    return RedirectToPage("/Cart");
                }

                EmptyBasket = false;


                CartItems = _productQuery.CheckInventoryStatus(CartItems);

                CartItems = _orderQuery.GetCartItemsBy(CartItems);

            }

            return RedirectToPage(CartItems.Any(x => !x.IsInStock) ? "/Cart" : "/Checkout");

        }
    }
}
