using AutoMapper;
using BORFinanceBusiness;
using BORFinanceCommon.Models;
using BORFinanceDomain.Entities.Security;
using BORFinanceDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BORFinanceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //[Authorize(Roles = "ADMIN")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();

            if (users == null)
                return NotFound("No users found.");

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "success.",
                Data = users
            });
        }

       
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(long id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
                return NotFound("User not found.");

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "success.",
                Data = user
            });
        }

       
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(UserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

           // var user = _mapper.Map<User>(dto);

            // Hash password using BCrypt
            var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var result = await _userService.CreateAsync(dto);

            if (!result)
                return BadRequest("Username already exists.");

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "User created successfully.",
                Data = result
            });

        }

      
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(long id, UserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _userService.GetByIdAsync(id);

            if (existing == null)
                return NotFound("User not found.");

            //_mapper.Map(dto, existing);

            var result = await _userService.UpdateAsync(existing);

            if (!result)
                return BadRequest("Unable to update user.");


            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "User updated successfully.",
                Data = result
            });
           
        }

     
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var result = await _userService.DeleteAsync(id);

            if (!result)
                return NotFound("User not found.");

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "User deleted successfully.",
                Data = result
            });
            
        }
    }
}
