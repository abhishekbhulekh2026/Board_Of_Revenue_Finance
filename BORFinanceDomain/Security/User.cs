using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BORFinanceDomain.Entities.Employees;
namespace BORFinanceDomain.Entities.Security
{
    public class User :ISoftDelete
    {
        public long Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string? MobileNumber { get; set; }

        public string? Email { get; set; }

        public long? DistrictId { get; set; }

        public string? ProfilePic { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public int FailedLoginAttempts { get; set; }

        public bool AccountLocked { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string? ApprovalStatus { get; set; }

        public long? ApprovedBy { get; set; }


        // Navigation Properties

        public virtual Employee? Employee { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
            = new List<UserRole>();

        public virtual ICollection<UserSession> UserSessions { get; set; }
            = new List<UserSession>();

        public virtual ICollection<LoginHistory> LoginHistories { get; set; }
            = new List<LoginHistory>();

       
    }
}
