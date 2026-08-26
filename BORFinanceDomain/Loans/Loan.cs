using BORFinanceDomain.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceDomain.Loans
{
    public class Loan
    {
        public long LoanId { get; set; }

        public long MembershipId { get; set; }

        public string LoanNumber { get; set; } = string.Empty;

        public string LoanType { get; set; } = string.Empty;

        public decimal RequestedAmount { get; set; }

        public decimal ApprovedAmount { get; set; }

        public decimal InterestRate { get; set; }

        public int TenureMonths { get; set; }

        public DateTime ApplicationDate { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public DateTime? DisbursementDate { get; set; }

        public string Status { get; set; } = "Pending";

        public string? Purpose { get; set; }

        public string? Remarks { get; set; }

        public long? ApprovedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public long? UpdatedBy { get; set; }

        // Navigation

        public virtual Membership Membership { get; set; } = null!;

        public virtual ICollection<LoanInstallment> Installments { get; set; }
            = new List<LoanInstallment>();
    }
}
