using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace _0_Framework.Application
{
    public class AuthHelper : IAuthHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public void SignIn(AuthViewModel account)
        {
            var claims = new List<Claim>
            {
                new Claim("AccountId", account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.FullName),
                new Claim(ClaimTypes.Role, account.RoleId.ToString()),
                new Claim("Username", account.Username), // Or Use ClaimTypes.NameIdentifier
                //new Claim("permissions", permissions),
                //new Claim("Mobile", account.)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
            };

            _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
        
        public void SignOut()
        {
            _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public bool IsAuthenticated()
        {
            //! Can do a lot of things here

            var claimsPrincipal = _contextAccessor.HttpContext.User.Claims.ToList();

            if (claimsPrincipal.Count > 0)
            {
                return true;
            }

            return false;
        }

        public string CurrentUserRole()
        {
            if (IsAuthenticated())
            {
                return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            }

            return null;
        }
    }
}