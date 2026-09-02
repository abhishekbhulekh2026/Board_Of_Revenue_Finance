using BORFinanceBusiness;
using BORFinanceCommon.Models;
using BORFinanceDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BORFinanceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoanController(
            ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet("GetAllLoans")]
        public async Task<IActionResult> GetAllLoans()
        {
            var result =
                await _loanService.GetAllAsync();

            if(result ==null)
                return NotFound("No loans found.");

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "success.",
                Data = result
            });
        }

        [HttpGet("GetLoanDetailsById")]
        public async Task<IActionResult> GetLoanDetailsById(
            long loanId)
        {
            var result =
                await _loanService.GetByIdAsync(
                    loanId);

            if (result == null)
                return NotFound("No loans found.");

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "success.",
                Data = result
            });
        }

        [HttpPost("CreateLoanApplication")]
        public async Task<IActionResult> CreateLoanApplication(
            [FromBody] LoanDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result =
                await _loanService.CreateAsync(dto);

            if (!result)
                return BadRequest(
                    "Loan could not be created.");

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Loan created successfully.",
                Data = result
            });
           
        }

        [HttpPut("UpdateLoanApplication")]
        public async Task<IActionResult> UpdateLoanApplication(
            long loanId,
            [FromBody] LoanDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            dto.LoanId = loanId;

            var result =
                await _loanService.UpdateAsync(dto);

            if (!result)
                return BadRequest(
                    "Loan could not be updated.");

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Loan updated successfully.",
                Data = result
            });
        }

        [HttpDelete("DeleteLoanApplication")]
        public async Task<IActionResult> DeleteLoanApplication(
            long loanId)
        {
            var result =
                await _loanService.DeleteAsync(
                    loanId);

            if (!result)
                return BadRequest(
                    "Loan could not be deleted.");

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Loan deleted successfully.",
                Data = result
            });
           
        }
    }
}
