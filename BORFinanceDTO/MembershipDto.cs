using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceDTO
{
   public class MembershipDto
    {
        public long MembershipId { get; set; }

        [Required]
        public long EmployeeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string MembershipNumber { get; set; }
            = string.Empty;

        [Required]
        public DateTime MembershipDate { get; set; }

        [Range(1, int.MaxValue)]
        public int ShareCount { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal ShareValue { get; set; }

        [MaxLength(50)]
        public string Status { get; set; }
            = "Active";
    }
}
