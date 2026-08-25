using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceDomain.Entities.Security
{
    public class RefreshToken
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        // SHA-256 hash of the actual refresh token
        public string TokenHash { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? RevokedAt { get; set; }

        public string? ReplacedByTokenHash { get; set; }

        public bool IsRevoked { get; set; }

        // Navigation
        public virtual User User { get; set; } = null!;
    }
}
