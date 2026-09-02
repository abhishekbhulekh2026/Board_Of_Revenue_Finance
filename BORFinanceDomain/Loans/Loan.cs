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

        // Member applying for the loan
        public long MembershipId { get; set; }

        public string LoanNumber { get; set; } = string.Empty;

        // FK instead of string
        public int LoanTypeId { get; set; }

        // Application
        public decimal RequestedAmount { get; set; }

        public DateTime ApplicationDate { get; set; }

        public string? Purpose { get; set; }

        public string? Remarks { get; set; }

        // Approved loan
        public decimal ApprovedAmount { get; set; }

        public decimal InterestRate { get; set; }

        public int TenureMonths { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public long? ApprovedBy { get; set; }

        // Disbursement
        public DateTime? DisbursementDate { get; set; }

        public decimal? PaidAmount { get; set; }

        public string? PaymentReference { get; set; }

        public string? BankTransactionReference { get; set; }

        public string? PaymentStatus { get; set; }

        // Current lifecycle status
        public string Status { get; set; } = "Pending";

        // Audit
        public DateTime CreatedAt { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public long? UpdatedBy { get; set; }

        // Navigation
        public virtual Membership Membership { get; set; } = null!;

        public virtual LoanType LoanType { get; set; } = null!;

        public virtual ICollection<LoanGuarantor> Guarantors { get; set; }
            = new List<LoanGuarantor>();

        public virtual ICollection<LoanInstallment> Installments { get; set; }
            = new List<LoanInstallment>();
    }









}
