using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceDomain.Entities.Security
{
    public class UserSession
    {
        public long UserSessionId { get; set; }

        public long UserId { get; set; }

        public string RefreshTokenHash { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime ExpiresAt { get; set; }

        public DateTime? RevokedAt { get; set; }

        public string? IpAddress { get; set; }

        public string? UserAgent { get; set; }

        public string? DeviceName { get; set; }

        public bool IsActive { get; set; }

        // Navigation

        public virtual User User { get; set; } = null!;
    }
}
