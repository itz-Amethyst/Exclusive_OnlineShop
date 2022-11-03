using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    public class EditModel : PageModel
    {
        private readonly IRoleApplication _roleApplication;

        public EditRole Command;

        public List<PermissionViewModel> Permission;

        public List<int> RolePermission;

        public EditModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        public void OnGet(int id)
        {
            Command = _roleApplication.GetDetails(id);
            Permission = _roleApplication.GetAllPermissions();
            RolePermission = _roleApplication.SelectedPermissionsRole(id);
        }

        public IActionResult OnPost(EditRole command, List<int> SelectedPermission)
        {
            var result = _roleApplication.Edit(command, SelectedPermission);

            return RedirectToPage("./Index", new { Edited = "True" });
        }
    }
}
