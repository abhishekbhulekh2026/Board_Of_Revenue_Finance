using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceDTO
{
    public class UserDto
    {
        public long Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; } = string.Empty;

        // Plain password from client.
        // Hash it in Business Layer before saving.
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string FullName { get; set; } = string.Empty;

        [Phone]
        [StringLength(20)]
        public string? MobileNumber { get; set; }

        [EmailAddress]
        [StringLength(200)]
        public string? Email { get; set; }

        public long? DistrictId { get; set; }

        [StringLength(1000)]
        public string? ProfilePic { get; set; }

        //[Required]
        //public int RoleId { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
