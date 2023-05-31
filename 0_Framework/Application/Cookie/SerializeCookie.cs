using Microsoft.AspNetCore.Http;
using Nancy.Json;

namespace _0_Framework.Application.Cookie
{
    public class SerializeCookie : ISerializeCookie
    {
        private const string CookieName = "cart-items";
        public List<CookieCartModel> CartItems;

        public List<CookieCartModel> DeSerialize(List<CookieCartModel> cartItems , HttpContext httpContext)
        {
            //    var value = serializer.Serialize(model);
            //    var cookieOptions = new CookieOptions
            //    {
            //        Expires = DateTime.Now.AddDays(1)
            //    };
            //    httpContext.Response.Cookies.Append(CookieName, value, cookieOptions);
            //    return true;
            //}

            var serializer = JavaScriptSerializer();
            var value = httpContext.Request.Cookies[CookieName];

            if (CheckSerialize(cartItems , httpContext))
            {
                cartItems = serializer.Deserialize<List<CookieCartModel>>(value);

                return cartItems;
            }

            return cartItems;
        }

        public bool CheckSerialize(List<CookieCartModel> cartItems, HttpContext httpContext)
        {
            var serializer = JavaScriptSerializer();
            var value = httpContext.Request.Cookies[CookieName];

            try
            {
                cartItems = serializer.Deserialize<List<CookieCartModel>>(value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                httpContext.Response.Cookies.Delete(CookieName);
                return false;
            }

            return true;
        }

        public JavaScriptSerializer JavaScriptSerializer()
        {
            return new JavaScriptSerializer();
        }

        public void DeleteCookie(HttpContext httpContext)
        {
            httpContext.Response.Cookies.Delete(CookieName);
        }

        public void RemoveItemFromCookie(List<CookieCartModel> cartItems, int id, HttpContext httpContext)
        {
            var serializer = JavaScriptSerializer();

            DeleteCookie(httpContext);

            if (CheckSerialize(cartItems, httpContext))
            {
                CartItems = DeSerialize(cartItems, httpContext);
                
                var itemToRemove = CartItems.FirstOrDefault(x => x.Id == id);
                CartItems.Remove(itemToRemove);

                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(7)
                };

                httpContext.Response.Cookies.Append(CookieName, serializer.Serialize(CartItems), cookieOptions);
            }
            //else
            //{
            //    DeleteCookie(httpContext);
            //}
           
        }
    }
}
