using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BORFinanceBusiness;
using BORFinanceDomain.Entities.Security;
using BORFinanceDTO;

namespace BORFinanceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize(Roles = "ADMIN")]
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

            return Ok(users);
        }

       
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(long id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
                return NotFound("User not found.");

            return Ok(user);
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

            return Ok("User created successfully.");
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

            return Ok("User updated successfully.");
        }

     
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var result = await _userService.DeleteAsync(id);

            if (!result)
                return NotFound("User not found.");

            return Ok("User deleted successfully.");
        }
    }
}
