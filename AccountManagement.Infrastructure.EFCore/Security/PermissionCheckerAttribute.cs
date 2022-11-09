using AccountManagement.Domain.RoleAgg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AccountManagement.Infrastructure.EFCore.Security
{
    public class PermissionCheckerAttribute : AuthorizeAttribute , IAuthorizationFilter
    {
        private IPermissionChecker _permissionChecker;

        private int _permissionId = 0;

        public PermissionCheckerAttribute(int permissionId)
        {
            _permissionId = permissionId;
        }
        
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _permissionChecker =
                (IPermissionChecker)context.HttpContext.RequestServices.GetService(typeof(IPermissionChecker));

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string UserName = context.HttpContext.User.Identity.Name;

                if (!_permissionChecker.CheckPermission(_permissionId, UserName))
                {
                    context.Result = new RedirectResult("/AccessDenied");
                }
            }
            else
            {
                context.Result = new RedirectResult("/Login");
            }
        }
    }
}
