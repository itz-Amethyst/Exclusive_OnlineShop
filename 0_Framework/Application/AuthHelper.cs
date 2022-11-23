using System.Security.Claims;
using _0_Framework.Infrastructure;
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
                //new Claim(ClaimTypes.Role, account.RoleId.ToString()),
                new Claim(ClaimTypes.Name , account.Username),
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                //new Claim("permissions", permissions),
                //new Claim("Mobile", account.)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(claimsIdentity);
            
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
                IsPersistent = account.RememberMe
            };

            _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
        }
        
        public void SignOut()
        {
            _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public bool IsAuthenticated()
        {
            return _contextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        //public string CurrentUserRole()
        //{
        //    if (IsAuthenticated())
        //    {
        //        return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
        //    }

        //    return null;
        //}

        //public AuthViewModel CurrentAccountInfo()
        //{
        //    var result = new AuthViewModel();

        //    if (!IsAuthenticated())
        //    {
        //        return result;
        //    }

        //    var claims = _contextAccessor.HttpContext.User.Claims.ToList();

        //    result.Id = int.Parse(claims.First(x => x.Type == "AccountId").Value);
        //    //result.Username = claims.First(x => x.Type == "Username").Value;
        //    result.RoleId = int.Parse(claims.First(x => x.Type == ClaimTypes.Role).Value);
        //    result.Role = Roles.GetRoleBy(result.RoleId);
        //    //!Can be used for email

        //    return result;
        //}

        public int CurrentAccountId()
        {
            return IsAuthenticated()
                ? int.Parse(_contextAccessor.HttpContext.User.Claims.First(x => x.Type == "AccountId")?.Value)
                : 0;
        }
    }
}