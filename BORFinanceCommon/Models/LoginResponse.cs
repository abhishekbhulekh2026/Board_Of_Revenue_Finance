using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceCommon.Models
{
    public class LoginResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public long UserId { get; set; }

        public string Username { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public int RoleId { get; set; }

        public string RoleCode { get; set; } = string.Empty;

        public string RoleName { get; set; } = string.Empty;

        public TokenResponse? Tokens { get; set; }
    }
}
