using BORFinanceBusiness;
using BORFinanceCommon.Models;
using BORFinanceDTO;
using BORFinanceRepository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BORFinanceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   // [Authorize]
    public class MembershipController : ControllerBase
    {
        private readonly IMembershipService _membershipService;

        public MembershipController(
            IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        [HttpGet("GetAllMembers")]
        public async Task<IActionResult> GetAllMembers()
        {
            var result =
                await _membershipService.GetAllAsync();


            if (result == null)
                return NotFound("No Member found.");

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "success.",
                Data = result
            });
        }

        [HttpGet("GetMemberById")]
        public async Task<IActionResult> GetMemberById(
            long membershipId)
        {
            var result =
                await _membershipService.GetByIdAsync(
                    membershipId);

            if (result == null)
                return NotFound("No Member found.");

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "success.",
                Data = result
            });
        }

        [HttpPost("CreateMember")]
        public async Task<IActionResult> CreateMember(
            [FromBody] MembershipDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result =
                await _membershipService.CreateAsync(dto);

            if (!result)
                return BadRequest(
                    "Membership could not be created.");

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Membership created successfully.",
                Data = result
            });
        
        }

        [HttpPut("UpdateMember")]
        public async Task<IActionResult> UpdateMember(
            long membershipId,
            [FromBody] MembershipDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            dto.MembershipId = membershipId;

            var result =
                await _membershipService.UpdateAsync(dto);

            if (!result)
                return BadRequest(
                    "Membership could not be updated.");

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Membership updated successfully.",
                Data = result
            });
        }

        [HttpDelete("DeleteMember")]
        public async Task<IActionResult> DeleteMember(
            long membershipId)
        {
            var result =
                await _membershipService.DeleteAsync(
                    membershipId);

            if (!result)
                return BadRequest(
                    "Membership could not be deleted.");

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Membership deleted successfully.",
                Data = result
            });

        }
    }
}
