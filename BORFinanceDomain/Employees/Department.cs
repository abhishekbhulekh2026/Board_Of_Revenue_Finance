using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceDomain.Entities.Employees
{
    public class Department
    {
        public int DepartmentId { get; set; }

        public string DepartmentCode { get; set; } = string.Empty;

        public string DepartmentName { get; set; } = string.Empty;

        public string? DepartmentNameHi { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public int SortOrder { get; set; }

        public DateTime CreatedAt { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public long? UpdatedBy { get; set; }

        // Navigation

        public virtual ICollection<Employee> Employees { get; set; }
            = new List<Employee>();
    }
}
