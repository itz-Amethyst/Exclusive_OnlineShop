using AccountManagement.Domain.AccountAgg;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace AccountManagement.Domain.RoleAgg
{
    public class Permission
    {
        public int Id { get; private set; }

        public string PermissionTitle { get; private set; }

        public int? ParentId { get; private set; }

        public Permission(string permissionTitle, int? parentId)
        {
            PermissionTitle = permissionTitle;
            ParentId = parentId;
        }

        [ForeignKey("ParentId")]
        public List<Permission> Permissions { get; private set; }

        public List<RolePermissions> RolePermissions { get; private set; }
        
    }

}
