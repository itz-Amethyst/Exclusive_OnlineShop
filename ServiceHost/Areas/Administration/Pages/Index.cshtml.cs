using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore.Security;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages
{
    [PermissionChecker(Roles.Administrator)]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
