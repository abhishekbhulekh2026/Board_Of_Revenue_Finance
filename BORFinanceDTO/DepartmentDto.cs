using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceDTO
{
    public class DepartmentDto
    {
        public int DepartmentId { get; set; }

        public string DepartmentCode { get; set; } = string.Empty;

        public string DepartmentName { get; set; } = string.Empty;

        public string? DepartmentNameHi { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }
    }
}
