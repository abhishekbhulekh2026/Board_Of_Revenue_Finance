using BORFinanceBusiness;
using BORFinanceCommon.Models;
using BORFinanceDomain.Entities.Employees;
using BORFinanceDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BORFinanceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly IDesignationService _designationService;

        public DesignationController(IDesignationService designationService)
        {
            _designationService = designationService;
        }

        [HttpGet("GetDesignationList")]
        public async Task<IActionResult> GetDesignationList()
        {
            var result = await _designationService.GetItemDtosAsync();
            if (result == null)
                return NotFound("No department found.");

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "success.",
                Data = result
            });
        }

        [HttpPost("AddDesignation")]
        public async Task<ActionResult> AddDesignation([FromBody] DesignationDto request)
        {
            var result = await _designationService.AddDesignation(request);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Designation created successfully.!",
                Data = result
            });
        }
    }
}
