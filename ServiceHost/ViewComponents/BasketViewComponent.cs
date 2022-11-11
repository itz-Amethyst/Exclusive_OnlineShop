using _01_ExclusiveQuery.Contracts.Order;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;

namespace ServiceHost.ViewComponents
{
    public class BasketViewComponent : ViewComponent
    {
        private readonly IOrderQuery _orderQuery;
        public List<CartQueryModel> CartItems;

        public BasketViewComponent(IOrderQuery orderQuery)
        {
            _orderQuery = orderQuery;
        }

        public IViewComponentResult Invoke()
        {
            if (Request.Cookies["cart-items"] != null)
            {
                var serializer = new JavaScriptSerializer();
                var value = Request.Cookies["cart-items"];
                CartItems = serializer.Deserialize<List<CartQueryModel>>(value);
                var basket = _orderQuery.GetCartItemsBy(CartItems);
                return View(basket);

            }

            return View(CartItems);
        }
    }
}
