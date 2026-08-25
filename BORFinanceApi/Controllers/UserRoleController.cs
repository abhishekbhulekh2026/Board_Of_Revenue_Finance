using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BORFinanceBusiness;
using BORFinanceDTO;

namespace BORFinanceApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    //[Authorize]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }



        [HttpGet("GetAllUserRole")]
        public async Task<IActionResult> GetAllUserRole()
        {
            var result = await _userRoleService
                .GetAllAsync();

            if (result == null)
                return NotFound("User role assignment not found.");

            return Ok(result);
        }


        [HttpGet("GetUserByRole")]
        public async Task<IActionResult> GetUserByRole(
            long userId,
            int roleId)
        {
            var result = await _userRoleService
                .GetAsync(userId, roleId);

            if (result == null)
                return NotFound("User role assignment not found.");

            return Ok(result);
        }


        [HttpGet("GetByUserId")]
        public async Task<IActionResult> GetByUserId(
            long userId)
        {
            var result = await _userRoleService
                .GetByUserIdAsync(userId);

            return Ok(result);
        }

       
        [HttpGet("GetByRoleId")]
        public async Task<IActionResult> GetByRoleId(
            int roleId)
        {
            var result = await _userRoleService
                .GetByRoleIdAsync(roleId);

            return Ok(result);
        }

       
        [HttpPost("AssignUserRoles")]
        public async Task<IActionResult> AssignUserRoles(
            UserRoleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userRoleService
                .CreateAsync(dto);

            if (!result)
                return BadRequest(
                    "Unable to assign role to user.");

            return Ok(
                "Role assigned to user successfully.");
        }

        
        [HttpPut("ReassignUserRoles")]
        public async Task<IActionResult> ReassignUserRoles(
            long userId,
            int roleId,
            UserRoleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Make sure route and body identify the same record
            if (dto.UserId != userId ||
                dto.RoleId != roleId)
            {
                return BadRequest(
                    "UserId and RoleId do not match.");
            }

            var result = await _userRoleService
                .UpdateAsync(dto);

            if (!result)
                return BadRequest(
                    "Unable to update user role.");

            return Ok(
                "User role updated successfully.");
        }

       
        [HttpDelete("DeleteAssignedUserRoles")]
        public async Task<IActionResult> DeleteAssignedUserRoles(
            long userId,
            int roleId)
        {
            var result = await _userRoleService
                .DeleteAsync(userId, roleId);

            if (!result)
                return BadRequest(
                    "Unable to remove user role.");

            return Ok(
                "User role removed successfully.");
        }

    }
}

       
     
