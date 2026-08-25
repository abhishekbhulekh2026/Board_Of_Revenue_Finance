using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BORFinanceBusiness;
using BORFinanceDTO;

[ApiController]
[Route("api/[controller]")]
//[Authorize]
public class PermissionController : ControllerBase
{
    private readonly IPermissionService _permissionService;

    public PermissionController(
        IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

   
    [HttpGet("GetPermissionTypeList")]
    public async Task<IActionResult> GetPermissionTypeList()
    {
        var result =
            await _permissionService.GetAllAsync();

        return Ok(result);
    }

   
    [HttpGet("GetPermissionById")]
    public async Task<IActionResult> GetPermissionById(
        int id)
    {
        var result =
            await _permissionService
                .GetByIdAsync(id);

        if (result == null)
            return NotFound(
                "Permission not found.");

        return Ok(result);
    }

   
    [HttpGet("GetActivePermissionTypes")]
    public async Task<IActionResult> GetActivePermissionTypes()
    {
        var result =
            await _permissionService
                .GetActiveAsync();

        return Ok(result);
    }

    
    [HttpPost("AddPermissionType")]
    public async Task<IActionResult> AddPermissionType(
        PermissionDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result =
            await _permissionService
                .CreateAsync(dto);

        if (!result)
            return BadRequest(
                "Unable to create permission.");

        return Ok(
            "Permission created successfully.");
    }

   
    [HttpPut("UpdatePermissionType")]
    public async Task<IActionResult> UpdatePermissionType(
        int id,
        PermissionDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (dto.PermissionId != id)
            return BadRequest(
                "PermissionId does not match.");

        var result =
            await _permissionService
                .UpdateAsync(dto);

        if (!result)
            return BadRequest(
                "Unable to update permission.");

        return Ok(
            "Permission updated successfully.");
    }

    
    [HttpDelete("DeletePermissionType")]
    public async Task<IActionResult> DeletePermissionType(
        int id)
    {
        var result =
            await _permissionService
                .DeleteAsync(id);

        if (!result)
            return BadRequest(
                "Unable to delete permission.");

        return Ok(
            "Permission deleted successfully.");
    }
}