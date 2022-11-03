namespace AccountManagement.Domain.RoleAgg
{
    public class RolePermissions
    {
        public int Id { get; private set; }

        public int RoleId { get; set; }

        public int PermissionId { get; set; }

        public Role Role { get; private set; }

        public Permission Permission { get; private set; }
    }
}
