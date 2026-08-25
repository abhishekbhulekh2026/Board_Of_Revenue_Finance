using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceDTO
{
    public class PermissionDto
    {
        public int PermissionId { get; set; }

        public string PermissionCode { get; set; } = string.Empty;

        public string PermissionName { get; set; } = string.Empty;

        public string? ModuleName { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }
    }
}
