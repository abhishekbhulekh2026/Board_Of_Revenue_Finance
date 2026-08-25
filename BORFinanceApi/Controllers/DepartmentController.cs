using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BORFinanceBusiness;
using BORFinanceDomain.Entities.Employees;
using BORFinanceDTO;

namespace BORFinanceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [HttpGet("GetDepartmentList")]
        public async Task<IActionResult> GetDepartmentList()
        {
            var items = await _departmentService.GetItemDtosAsync();
            return Ok(items);
        }

        [HttpPost("AddDepartment")]
        public async Task<IActionResult> AddDepartment([FromBody] DepartmentDto departmentDto)
        {
            await _departmentService.AddDepartmentAsync(departmentDto);
            return Ok();
        }
    }
}
