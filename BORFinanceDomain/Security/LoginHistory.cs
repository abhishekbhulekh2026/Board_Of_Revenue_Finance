using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceDomain.Entities.Security
{
    public class LoginHistory
    {
        public long LoginHistoryId { get; set; }

        public long? UserId { get; set; }

        public string Username { get; set; } = string.Empty;

        public bool IsSuccessful { get; set; }

        public string? FailureReason { get; set; }

        public DateTime LoginDate { get; set; }

        public string? IpAddress { get; set; }

        public string? UserAgent { get; set; }

        public string? DeviceName { get; set; }

        // Navigation

        public virtual User? User { get; set; }
    }
}
