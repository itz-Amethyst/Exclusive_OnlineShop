using _01_ExclusiveQuery.Contracts.Order;
using _01_ExclusiveQuery.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages
{
    public class CartModel : PageModel
    {
        private readonly IOrderQuery _orderQuery;

        [TempData] public string CartMessage { get; set; }

        public List<CartQueryModel> CartItems;
        public const string CookieName = "cart-items";

        public CartModel(IOrderQuery orderQuery)
        {
            _orderQuery = orderQuery;
        }

        public void OnGet()
        {

            if (Request.Cookies["cart-items"] != null)
            {
                var serializer = new JavaScriptSerializer();
                var value = Request.Cookies[CookieName];
                CartItems = serializer.Deserialize<List<CartQueryModel>>(value);
                if (CartItems.Count <= 0)
                {
                    CartMessage = "t";
                    Response.Cookies.Delete(CookieName);
                    return;
                }

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
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            Response.Cookies.Delete(CookieName);
            CartItems = serializer.Deserialize<List<CartQueryModel>>(value);
            var itemToRemove = CartItems.FirstOrDefault(x => x.Id == id);
            CartItems.Remove(itemToRemove);

            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(2)
            };

            Response.Cookies.Append(CookieName, serializer.Serialize(CartItems), cookieOptions);

            return RedirectToPage("./Cart");
        }
    }
}
