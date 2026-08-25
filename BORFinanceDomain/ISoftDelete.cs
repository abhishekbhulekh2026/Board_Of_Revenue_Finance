using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceDomain.Entities
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
