namespace AccountManagement.Domain.RoleAgg
{
    public class RolePermissions
    {
        public int Id { get; private set; }

        public int RoleId { get; private set; }

        public int PermissionId { get; private set; }

        public RolePermissions(int roleId, int permissionId)
        {
            RoleId = roleId;
            PermissionId = permissionId;
        }


        public Role Role { get; private set; }

        public Permission Permission { get; private set; }
    }
}
