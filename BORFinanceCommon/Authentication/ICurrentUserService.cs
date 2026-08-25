using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceCommon.Authentication
{
    public interface ICurrentUserService
    {
        long? UserId { get; }

        bool IsAuthenticated { get; }
    }
}
