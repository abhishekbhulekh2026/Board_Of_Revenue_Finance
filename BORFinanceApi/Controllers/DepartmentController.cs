using BORFinanceBusiness;
using BORFinanceCommon.Models;
using BORFinanceDomain.Entities.Employees;
using BORFinanceDomain.Entities.Security;
using BORFinanceDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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

            if(items==null)
                return NotFound("No department found.");

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "success.",
                Data = items
            });
        }

        [HttpPost("AddDepartment")]
        public async Task<IActionResult> AddDepartment([FromBody] DepartmentDto departmentDto)
        {
            var result  = await _departmentService.AddDepartmentAsync(departmentDto);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Department created successfully.!",
                Data = result
            });
           
        }
    }
}
