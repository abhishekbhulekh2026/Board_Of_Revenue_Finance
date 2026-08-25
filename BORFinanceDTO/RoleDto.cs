using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceDTO
{
    public class RoleDto
    {
        public int RoleId { get; set; }

        [Required]
        [StringLength(30)]
        public string RoleCode { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string RoleName { get; set; } = string.Empty;

        public string? RoleDescription { get; set; }

        public int? ParentRoleId { get; set; }

        public byte RoleLevel { get; set; }

        public int SortOrder { get; set; }

        public bool IsAssignable { get; set; }

        public bool IsActive { get; set; }

        public string? RoleNameHi { get; set; }

        public string? RoleDescriptionHi { get; set; }
    }
}
