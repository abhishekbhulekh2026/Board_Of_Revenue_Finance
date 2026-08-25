using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BORFinanceBusiness;
using BORFinanceDTO;

[ApiController]
[Route("api/[controller]")]
//[Authorize]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(
        IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet("GetEmployeeList")]
    public async Task<IActionResult> GetEmployeeList()
    {
        var result =
            await _employeeService.GetAllAsync();

        return Ok(result);
    }

   
    [HttpGet("GetActiveEmployees")]
    public async Task<IActionResult> GetActiveEmployees()
    {
        var result =
            await _employeeService.GetActiveAsync();

        return Ok(result);
    }

    [HttpGet("GetEmployeeById")]
    public async Task<IActionResult> GetEmployeeById(
        long id)
    {
        var result =
            await _employeeService
                .GetByIdAsync(id);

        if (result == null)
            return NotFound(
                "Employee not found.");

        return Ok(result);
    }

    //[HttpGet("details/{id:long}")]
    //public async Task<IActionResult> GetDetails(
    //    long id)
    //{
    //    var result =
    //        await _employeeService
    //            .GetDetailsAsync(id);

    //    if (result == null)
    //        return NotFound(
    //            "Employee not found.");

    //    return Ok(result);
    //}

  
    //[HttpGet("details")]
    //public async Task<IActionResult> GetAllDetails()
    //{
    //    var result =
    //        await _employeeService
    //            .GetAllDetailsAsync();

    //    return Ok(result);
    //}

   
    [HttpPost("AddEmployee")]
    public async Task<IActionResult> AddEmployee(
        EmployeeDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result =
            await _employeeService
                .CreateAsync(dto);

        if (!result)
            return BadRequest(
                "Unable to create employee.");

        return Ok(
            "Employee created successfully.");
    }

    
    [HttpPut("UpdateEmployee")]
    public async Task<IActionResult> UpdateEmployee(
        long id,
        EmployeeDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (dto.EmployeeId != id)
            return BadRequest(
                "EmployeeId does not match.");

        var result =
            await _employeeService
                .UpdateAsync(dto);

        if (!result)
            return BadRequest(
                "Unable to update employee.");

        return Ok(
            "Employee updated successfully.");
    }

    [HttpDelete("DeleteEmployee")]
    public async Task<IActionResult> DeleteEmployee(
        long id)
    {
        var result =
            await _employeeService
                .DeleteAsync(id);

        if (!result)
            return BadRequest(
                "Unable to delete employee.");

        return Ok(
            "Employee deleted successfully.");
    }
}