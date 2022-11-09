using _0_Framework.Application;
using AccountManagement.Infrastructure.EFCore.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AccountManagement.Infrastructure.EFCore.TagHelper
{
    [HtmlTargetElement(Attributes = "Permission")]
    public class PermissionTagHelper : Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
    {
        public int Permission { get; set; }

        private readonly IAuthHelper _authHelper;
        private readonly IPermissionChecker _permissionChecker;
        private readonly IHttpContextAccessor _contextAccessor;

        public PermissionTagHelper(IAuthHelper authHelper, IPermissionChecker permissionChecker, IHttpContextAccessor contextAccessor)
        {
            _authHelper = authHelper;
            _permissionChecker = permissionChecker;
            _contextAccessor = contextAccessor;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!_authHelper.IsAuthenticated())
            {
                output.SuppressOutput();
                return;
            }

            var username = _contextAccessor.HttpContext.User.Identity.Name;

            var permissions = _permissionChecker.CheckPermission(Permission, username);
            if (permissions == false)
            {
                output.SuppressOutput();
                return;
            }

            base.Process(context, output);
        }
    }
}
