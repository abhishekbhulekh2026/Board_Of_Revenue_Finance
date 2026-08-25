using BORFinanceBusiness;
using BORFinanceCommon.Models;
using BORFinanceDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BORFinanceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }


        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var role = await _roleService.GetAllAsync();

            if (role == null)
                return NotFound();

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "success.",
                Data = role
            });
        }


        [HttpGet("GetRoleById")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await _roleService.GetByIdAsync(id);

            if (role == null)
                return NotFound();

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "success.",
                Data = role
            });
        }


        [HttpGet("GetActiveRoles")]
        public async Task<IActionResult> GetActiveRoles()
        {
            var roles = await _roleService.GetActiveRolesAsync();

            if (roles.Count == 0)
                return NotFound();

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "success.",
                Data = roles
            });


        }


        [HttpPost("CreateRole")]
        public async Task<IActionResult> Create(RoleDto roleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _roleService.CreateAsync(roleDto);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Role created successfully.",
                Data = result
            });
        }


        [HttpPut("UpdateRole")]
        public async Task<IActionResult> UpdateRole(int id, RoleDto roleDto)
        {
            if (id != roleDto.RoleId)
                return BadRequest("Invalid Role Id.");

            var result = await _roleService.UpdateAsync(roleDto);

            //if (!result)
            //    return NotFound();

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Role updated successfully.",
            });
        }


        [HttpDelete("DeleteRole")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var result = await _roleService.DeleteAsync(id);

            //if (!result)
            //    return NotFound();

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Role deleted successfully.",
            });

        }
    }
}
