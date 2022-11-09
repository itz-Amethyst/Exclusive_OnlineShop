namespace AccountManagement.Application.Contracts.Role
{
    public class PermissionViewModel
    {
        public int PermissionId { get; set; }

        public string PermissionTitle { get; set; }

        public int? ParentId { get; set; }
    }
}
