using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BORFinanceDTO
{
    public class LoanDto
    {
        public long LoanId { get; set; }

        [Required]
        public long MembershipId { get; set; }

        [Required]
        [MaxLength(50)]
        public string LoanNumber { get; set; } = string.Empty;

        [Required]
        public int LoanTypeId { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal RequestedAmount { get; set; }

        public decimal ApprovedAmount { get; set; }

        [Range(0, double.MaxValue)]
        public decimal InterestRate { get; set; }

        [Range(1, int.MaxValue)]
        public int TenureMonths { get; set; }

        public DateTime ApplicationDate { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public DateTime? DisbursementDate { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = "Pending";

        [MaxLength(500)]
        public string? Purpose { get; set; }

        [MaxLength(1000)]
        public string? Remarks { get; set; }

        public string? BankTransactionReference { get; set; }
    }
}
