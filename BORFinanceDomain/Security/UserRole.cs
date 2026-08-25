using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceDomain.Entities.Security
{
    public class UserRole
    {
        public long UserId { get; set; }

        public int RoleId { get; set; }

        public DateTime AssignedAt { get; set; }

        public long? AssignedBy { get; set; }

        public DateTime? RevokedAt { get; set; }

        public long? RevokedBy { get; set; }

        public bool IsActive { get; set; }

        // Navigation

        public virtual User User { get; set; } = null!;

        public virtual Role Role { get; set; } = null!;
    }
}
