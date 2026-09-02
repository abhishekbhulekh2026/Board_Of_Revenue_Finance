using BORFinanceBusiness;
using BORFinanceDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BORFinanceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanGuarantorController : ControllerBase
    {
        private readonly ILoanGuarantorService _loanGuarantorService;

        public LoanGuarantorController(
            ILoanGuarantorService loanGuarantorService)
        {
            _loanGuarantorService = loanGuarantorService;
        }

        [HttpGet("GetAllLoanGuarantors")]
        public async Task<IActionResult> GetAllLoanGuarantors()
        {
            var result =
                await _loanGuarantorService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("GetLoanGuarantorById/{id}")]
        public async Task<IActionResult> GetLoanGuarantorById(long id)
        {
            var result =
                await _loanGuarantorService.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("AddLoanGuarantor")]
        public async Task<IActionResult> AddLoanGuarantor(
            [FromBody] LoanGuarantorDto dto)
        {
            var result =
                await _loanGuarantorService.AddAsync(dto);

            return result
                ? Ok()
                : BadRequest();
        }

        [HttpPut("UpdateLoanGuarantor")]
        public async Task<IActionResult> UpdateLoanGuarantor(
            [FromBody] LoanGuarantorDto dto)
        {
            var result =
                await _loanGuarantorService.UpdateAsync(dto);

            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpDelete("DeleteLoanGuarantor/{id}")]
        public async Task<IActionResult> DeleteLoanGuarantor(long id)
        {
            var result =
                await _loanGuarantorService.DeleteAsync(id);

            if (!result)
                return NotFound();

            return Ok();
        }
    }
}
