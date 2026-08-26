using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceDomain.Loans
{
    public class LoanInstallment
    {
        public long LoanInstallmentId { get; set; }

        public long LoanId { get; set; }

        public int InstallmentNumber { get; set; }

        public DateTime DueDate { get; set; }

        public decimal PrincipalAmount { get; set; }

        public decimal InterestAmount { get; set; }

        public decimal InstallmentAmount { get; set; }

        public decimal PaidAmount { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; }

        // Navigation

        public virtual Loan Loan { get; set; } = null!;
    }
}
