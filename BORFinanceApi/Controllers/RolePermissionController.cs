using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BORFinanceBusiness;
using BORFinanceDTO;

namespace BORFinanceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   // [Authorize]
    public class RolePermissionController : ControllerBase
    {
        private readonly IRolePermissionService _service;
      

        public RolePermissionController(
            IRolePermissionService service)
        {
            _service = service;
        }

        [HttpGet("GetPermissionList")]
        public async Task<IActionResult> GetPermissionList(
            int roleId,
            int permissionId)
        {
            var result = await _service
                .GetAsync(roleId, permissionId);

            if (result == null)
                return NotFound(
                    "Role permission not found.");
            return Ok(result);
        }

        [HttpGet("GetPermissionByRoleId")]
        public async Task<IActionResult> GetPermissionByRoleId(
            int roleId)
        {
            var result = await _service
                .GetByRoleIdAsync(roleId);

            return Ok(result);
        }

        [HttpGet("GetPermissionById")]
        public async Task<IActionResult> GetPermissionById(
            int permissionId)
        {
            var result = await _service
                .GetByPermissionIdAsync(permissionId);

            return Ok(result);
        }

        [HttpPost("AddPermissions")]
        public async Task<IActionResult> AddPermissions(
            RolePermissionDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service
                .CreateAsync(dto);

            if (!result)
                return BadRequest(
                    "Unable to assign permission to role.");

            return Ok(
                "Permission assigned to role successfully.");
        }

        
        [HttpPut("UpdatePermission")]
        public async Task<IActionResult> UpdatePermission(
            int roleId,
            int permissionId,
            RolePermissionDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dto.RoleId != roleId ||
                dto.PermissionId != permissionId)
            {
                return BadRequest(
                    "RoleId and PermissionId do not match.");
            }

            var result = await _service
                .UpdateAsync(dto);

            if (!result)
                return BadRequest(
                    "Unable to update role permission.");

            return Ok(
                "Role permission updated successfully.");
        }

      
        [HttpDelete("DeletePermission")]
        public async Task<IActionResult> DeletePermission(
            int roleId,
            int permissionId)
        {
            var result = await _service
                .DeleteAsync(roleId, permissionId);

            if (!result)
                return BadRequest(
                    "Unable to remove role permission.");

            return Ok(
                "Role permission removed successfully.");
        }
    }
}