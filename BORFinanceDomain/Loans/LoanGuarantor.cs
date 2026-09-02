using BORFinanceDomain.Entities.Employees;
using BORFinanceDomain.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceDomain.Loans
{
    public class LoanGuarantor
    {
        public long LoanGuarantorId { get; set; }

        public long LoanId { get; set; }

        public long? MembershipId { get; set; }

        public long? EmployeeId { get; set; }

        public string GuarantorName { get; set; } = string.Empty;

        public string? Relationship { get; set; }

        public string? MobileNumber { get; set; }

        public string? Address { get; set; }

        public bool IsApproved { get; set; }

        public DateTime? ApprovedAt { get; set; }

        public long? ApprovedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public long? UpdatedBy { get; set; }

        // Navigation
        public virtual Loan Loan { get; set; } = null!;

        public virtual Membership? Membership { get; set; }

        public virtual Employee? Employee { get; set; }
    }
}
