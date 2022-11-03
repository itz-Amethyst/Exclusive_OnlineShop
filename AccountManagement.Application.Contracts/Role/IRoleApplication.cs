using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Role
{
    public interface IRoleApplication
    {
        OperationResult Create(CreateRole command, List<int> permissions);

        OperationResult Edit(EditRole command);

        EditRole GetDetails(int id);

        List<RoleViewModel> List();

        OperationResult Remove(int id);

        OperationResult Restore(int id);

        List<PermissionViewModel> GetAllPermissions();

        List<int> SelectedPermissionsRole(int id);
    }
}
