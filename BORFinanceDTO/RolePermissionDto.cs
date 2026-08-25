using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceDTO
{
    public class RolePermissionDto
    {
        public int RoleId { get; set; }

        public int PermissionId { get; set; }

        public bool IsAllowed { get; set; }

        public DateTime AssignedAt { get; set; }

        public long? AssignedBy { get; set; }
    }
}
