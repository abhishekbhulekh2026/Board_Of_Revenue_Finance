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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result =
                await _loanService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{loanId:long}")]
        public async Task<IActionResult> GetById(
            long loanId)
        {
            var result =
                await _loanService.GetByIdAsync(
                    loanId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] LoanDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result =
                await _loanService.CreateAsync(dto);

            if (!result)
                return BadRequest(
                    "Loan could not be created.");

            return Ok(new
            {
                message = "Loan created successfully."
            });
        }

        [HttpPut("{loanId:long}")]
        public async Task<IActionResult> Update(
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

            return Ok(new
            {
                message = "Loan updated successfully."
            });
        }

        [HttpDelete("{loanId:long}")]
        public async Task<IActionResult> Delete(
            long loanId)
        {
            var result =
                await _loanService.DeleteAsync(
                    loanId);

            if (!result)
                return BadRequest(
                    "Loan could not be deleted.");

            return Ok(new
            {
                message = "Loan deleted successfully."
            });
        }
    }
}
