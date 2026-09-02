using BORFinanceBusiness;
using BORFinanceDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BORFinanceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanTypeController : ControllerBase
    {
        private readonly ILoanTypeService _loanTypeService;

        public LoanTypeController(
            ILoanTypeService loanTypeService)
        {
            _loanTypeService = loanTypeService;
        }

        [HttpGet("GetAllLoanTypes")]
        public async Task<IActionResult> GetAllLoanTypes()
        {
            var result = await _loanTypeService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("GetLoanTypeById/{id}")]
        public async Task<IActionResult> GetLoanTypeById(int id)
        {
            var result = await _loanTypeService.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("AddLoanType")]
        public async Task<IActionResult> AddLoanType(
            [FromBody] LoanTypeDto dto)
        {
            var result = await _loanTypeService.AddAsync(dto);

            return result
                ? Ok()
                : BadRequest();
        }

        [HttpPut("UpdateLoanType")]
        public async Task<IActionResult> UpdateLoanType(
            [FromBody] LoanTypeDto dto)
        {
            var result = await _loanTypeService.UpdateAsync(dto);

            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpDelete("DeleteLoanType/{id}")]
        public async Task<IActionResult> DeleteLoanType(int id)
        {
            var result = await _loanTypeService.DeleteAsync(id);

            if (!result)
                return NotFound();

            return Ok();
        }
    }
}
