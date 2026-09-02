using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceDTO
{
    public class LoanGuarantorDto
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
    }
}
