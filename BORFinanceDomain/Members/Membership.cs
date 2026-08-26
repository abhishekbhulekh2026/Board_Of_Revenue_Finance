using BORFinanceDomain.Entities;
using BORFinanceDomain.Entities.Employees;
using BORFinanceDomain.FixedDeposits;
using BORFinanceDomain.Loans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceDomain.Members
{
    public class Membership : ISoftDelete
    {
        public long MembershipId { get; set; }

        public long EmployeeId { get; set; }

        public string MembershipNumber { get; set; } = string.Empty;

        public DateTime MembershipDate { get; set; }

        public int ShareCount { get; set; }

        public decimal ShareValue { get; set; }

        public string Status { get; set; } = "Active";

        public DateTime CreatedAt { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public long? UpdatedBy { get; set; }

        // Navigation
        public virtual Employee Employee { get; set; } = null!;

        public virtual ICollection<Loan> Loans { get; set; }
            = new List<Loan>();

        public virtual ICollection<FixedDeposit> FixedDeposits { get; set; }
            = new List<FixedDeposit>();
       
    }
}
