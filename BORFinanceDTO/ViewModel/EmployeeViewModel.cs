using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceDTO.ViewModel
{
    public class EmployeeViewModel
    {
        public long EmployeeId { get; set; }

        public long? UserId { get; set; }

        public string? Username { get; set; }

        public string EmployeeCode { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string? FullNameHi { get; set; }

        public string? FatherName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? MobileNumber { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public DateTime DateOfJoining { get; set; }

        public decimal BasicSalary { get; set; }

        public int DepartmentId { get; set; }

        public string? DepartmentName { get; set; }

        public int DesignationId { get; set; }

        public string? DesignationName { get; set; }

        public string? EmployeeStatus { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public long? UpdatedBy { get; set; }
    }
}
