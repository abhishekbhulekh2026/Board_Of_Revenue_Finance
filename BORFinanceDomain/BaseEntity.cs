using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceDomain.Entities
{
    public abstract class BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public long? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public long? UpdatedBy { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
