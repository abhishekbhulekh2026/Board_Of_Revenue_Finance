using BORFinanceDomain.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceDomain.FixedDeposits
{
    public class FixedDeposit
    {
        public long FixedDepositId { get; set; }

        public long MembershipId { get; set; }

        public string FDNumber { get; set; } = string.Empty;

        public decimal DepositAmount { get; set; }

        public decimal InterestRate { get; set; }

        public int TenureMonths { get; set; }

        public DateTime DepositDate { get; set; }

        public DateTime MaturityDate { get; set; }

        public decimal MaturityAmount { get; set; }

        public string InterestPayoutType { get; set; } = string.Empty;

        public string Status { get; set; } = "Active";

        public DateTime? ClosedDate { get; set; }

        public decimal? ClosedAmount { get; set; }

        public string? Remarks { get; set; }

        public DateTime CreatedAt { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public long? UpdatedBy { get; set; }

        // Navigation

        public virtual Membership Membership { get; set; } = null!;
    }
}
