using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceDomain.Entities.Security
{
    public class Role
    {
        public int RoleId { get; set; }

        public string RoleCode { get; set; } = string.Empty;

        public string RoleName { get; set; } = string.Empty;

        public string? RoleDescription { get; set; }

        public int? ParentRoleId { get; set; }

        public byte RoleLevel { get; set; }

        public int SortOrder { get; set; }

        public bool IsSystemRole { get; set; }

        public bool IsAssignable { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? DeactivatedAt { get; set; }

        public long? DeactivatedBy { get; set; }

        public string? RoleNameHi { get; set; }

        public string? RoleDescriptionHi { get; set; }

        // Self reference
        public virtual Role? ParentRole { get; set; }

        public virtual ICollection<Role> ChildRoles { get; set; }
            = new List<Role>();

        // Users
        public virtual ICollection<UserRole> UserRoles { get; set; }
            = new List<UserRole>();

        // Permissions
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
            = new List<RolePermission>();
    }
}
