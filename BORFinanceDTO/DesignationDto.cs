using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceDTO
{
    public class DesignationDto
    {
        public int DesignationId { get; set; }

        public string DesignationCode { get; set; } = string.Empty;

        public string DesignationName { get; set; } = string.Empty;

        public string? DesignationNameHi { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }
    }
}
