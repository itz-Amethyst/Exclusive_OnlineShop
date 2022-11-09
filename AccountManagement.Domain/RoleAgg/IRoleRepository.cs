using _0_Framework.Domain;
using AccountManagement.Application.Contracts.Role;

namespace AccountManagement.Domain.RoleAgg
{
    public interface IRoleRepository : IRepository<int , Role>
    {
        EditRole GetDetails(int id);

        List<RoleViewModel> List();
        
        List<PermissionViewModel> GetAllPermissions();
        
        bool AddPermissionsToRole(int roleId, List<int> permissions);

        List<int> SelectedPermissionsRole(int roleId);
        
        bool UpdatePermissionsRole(int roleId, List<int> permissions);

    }
}
