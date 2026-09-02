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
    public class LoanInstallmentController
    : ControllerBase
    {
        private readonly ILoanInstallmentService
            _installmentService;

        public LoanInstallmentController(
            ILoanInstallmentService installmentService)
        {
            _installmentService = installmentService;
        }

        [HttpGet("GetInstallmentsByLoanId")]
        public async Task<IActionResult> GetInstallmentsByLoanId(long LoanId)
        {
            var result =
                await _installmentService.GetLoanInstallmentsByLoanIdAsync(LoanId);

            if(result == null)
                return NotFound("No installments found for the given loan ID.");

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "success.",
                Data = result
            });
        }

        [HttpGet("GetInstallmentsById")]
        public async Task<IActionResult> GetInstallmentsById(
            long installmentId)
        {
            var result =
                await _installmentService
                    .GetByIdAsync(installmentId);

            if (result == null)
                return NotFound();

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Loan deleted successfully.",
                Data = result
            });
        }

        [HttpPost("CreateInstallments")]
        public async Task<IActionResult> CreateInstallments(
            [FromBody] LoanInstallmentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result =
                await _installmentService
                    .CreateAsync(dto);

            if (!result)
            {
                return BadRequest(
                    "Loan installment could not be created.");
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Loan installment created successfully.",
                Data = result
            });
        }

        [HttpPut("UpdateInstallments")]
        public async Task<IActionResult> UpdateInstallments(
            long installmentId,
            [FromBody] LoanInstallmentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            dto.LoanInstallmentId =
                installmentId;

            var result =
                await _installmentService
                    .UpdateAsync(dto);

            if (!result)
            {
                return BadRequest(
                    "Loan installment could not be updated.");
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Loan installment updated successfully.",
                Data = result
            });
        }

        [HttpDelete("DeleteInstallments")]
        public async Task<IActionResult> DeleteInstallments(
            long installmentId)
        {
            var result =
                await _installmentService
                    .DeleteAsync(installmentId);

            if (!result)
            {
                return BadRequest(
                    "Loan installment could not be deleted.");
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Loan installment deleted successfully.",
                Data = result
            });
        }
    }
}
