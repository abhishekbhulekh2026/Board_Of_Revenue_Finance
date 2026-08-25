using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceDomain.Entities.Security
{
    public class Permission
    {
        public int PermissionId { get; set; }

        public string PermissionCode { get; set; } = string.Empty;

        public string PermissionName { get; set; } = string.Empty;

        public string? ModuleName { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public long? CreatedBy { get; set; }

        // Navigation

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
            = new List<RolePermission>();
    }
}
