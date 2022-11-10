using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages
{
    public class CartModel : PageModel
    {
        public List<CartItem> CartItems;

        public void OnGet()
        {
            var serializer = new JavaScriptSerializer();

            var value = Request.Cookies["cart-items"];
            //HttpContext.Response.Cookies.Append("f" , "fa" , new CookieOptions
            //{
            //    HttpOnly = true
            //});
            CartItems = serializer.Deserialize<List<CartItem>>(value);

            foreach (var item in CartItems)
            {
                item.TotalItemPrice = item.UnitPrice * item.Count;
            }

            //Response.Cookies.Append("test" , serializer , cookieOptions);

            //? Only check first product work on this
            //List<Productv> product = _productRepository.GetById(CartItems[0].Id);

            //if (_productRepository.Exists(x => product == CartItems[0]))
            //{

            //}
        }
    }
}
