using _01_ExclusiveQuery.Contracts.Order;
using _01_ExclusiveQuery.Query;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages
{
    public class CartModel : PageModel
    {
        private readonly IOrderQuery _orderQuery;

        public List<CartQueryModel> CartItems;

        public CartModel(IOrderQuery orderQuery)
        {
            _orderQuery = orderQuery;
        }

        public void OnGet()
        {

            if (Request.Cookies["cart-items"] != null)
            {
                var serializer = new JavaScriptSerializer();
                var value = Request.Cookies["cart-items"];
                CartItems = serializer.Deserialize<List<CartQueryModel>>(value);
                CartItems = _orderQuery.GetCartItemsBy(CartItems);
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
