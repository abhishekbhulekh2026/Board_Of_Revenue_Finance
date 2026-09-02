using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BORFinanceDTO
{
    public class LoanInstallmentDto
    {
        public long LoanInstallmentId { get; set; }

        [Required]
        public long LoanId { get; set; }

        [Range(1, int.MaxValue)]
        public int InstallmentNumber { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Range(0, double.MaxValue)]
        public decimal PrincipalAmount { get; set; }

        [Range(0, double.MaxValue)]
        public decimal InterestAmount { get; set; }

        [Range(0, double.MaxValue)]
        public decimal InstallmentAmount { get; set; }

        [Range(0, double.MaxValue)]
        public decimal PaidAmount { get; set; }

        public DateTime? PaymentDate { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = "Pending";

        [MaxLength(30)]
        public string? PaymentMode { get; set; }

        [MaxLength(100)]
        public string? ChequeNumber { get; set; }

        public DateTime? ChequeDate { get; set; }
    }
}
