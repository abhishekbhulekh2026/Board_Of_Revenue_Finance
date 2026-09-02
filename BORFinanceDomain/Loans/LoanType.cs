using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceDomain.Loans
{
    public class LoanType
    {
        public int LoanTypeId { get; set; }

        public string LoanTypeCode { get; set; } = string.Empty;

        public string LoanTypeName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal? InterestRate { get; set; }

        public int? MaximumTenureMonths { get; set; }

        public decimal? MaximumLoanAmount { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; }

        public long? CreatedBy { get; set; }

        public virtual ICollection<Loan> Loans { get; set; }
            = new List<Loan>();
    }
}
