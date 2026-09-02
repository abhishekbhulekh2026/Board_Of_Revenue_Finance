using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceDTO
{
    public class LoanTypeDto
    {
        public int LoanTypeId { get; set; }

        public string LoanTypeCode { get; set; } = string.Empty;

        public string LoanTypeName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsActive { get; set; }
    }
}
