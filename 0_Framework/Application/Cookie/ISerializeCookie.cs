using Microsoft.AspNetCore.Http;
using Nancy.Json;

namespace _0_Framework.Application.Cookie
{
    public interface ISerializeCookie
    {
        List<CookieCartModel> DeSerialize(List<CookieCartModel> cartItems , HttpContext httpContext);

        bool CheckSerialize(List<CookieCartModel> cartItems, HttpContext httpContext);

        JavaScriptSerializer JavaScriptSerializer();

        void DeleteCookie(HttpContext httpContext);

        void RemoveItemFromCookie(List<CookieCartModel> cartItems , int id , HttpContext httpContext);
    }
}
