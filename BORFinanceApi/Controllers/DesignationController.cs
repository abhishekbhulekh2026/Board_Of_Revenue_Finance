using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BORFinanceBusiness;
using BORFinanceDomain.Entities.Employees;
using BORFinanceDTO;

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
            return Ok(result);
        }

        [HttpPost("AddDesignation")]
        public async Task<ActionResult> AddDesignation([FromBody] DesignationDto request)
        {
            await _designationService.AddDesignation(request);
            return Ok();
        }
    }
}
