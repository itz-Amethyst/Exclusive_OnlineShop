using AccountManagement.Infrastructure.EFCore.Context;

namespace AccountManagement.Infrastructure.EFCore.Security
{
    public class PermissionChecker : IPermissionChecker
    {
        private readonly AccountContext _accountContext;

        public PermissionChecker(AccountContext accountContext)
        {
            _accountContext = accountContext;
        }

        public bool CheckPermission(int permissionId, string username)
        {
            int userId = _accountContext.Accounts.Single(x => x.Username == username).Id;


            List<int> UserRoles = _accountContext.Accounts
                .Where(r => r.Id == userId)
                .Select(u => u.RoleId).ToList();
            if (!UserRoles.Any())
                return false;

            List<int> RolesPermission = _accountContext.RolePermissions
                .Where(p => p.PermissionId == permissionId)
                .Select(p => p.RoleId).ToList();

            return RolesPermission.Any(p => UserRoles.Contains(p));
        }
    }
}
