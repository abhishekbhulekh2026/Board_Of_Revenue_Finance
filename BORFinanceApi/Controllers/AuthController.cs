using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using BORFinanceBusiness;
using BORFinanceCommon.Models;
using BORFinanceDomain.Entities.Security;
namespace BORFinanceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody]  BORFinanceCommon.Models.LoginRequest request)
        {
           
            var resonse = await _authService.LoginAsync(request);
            return Ok(resonse);
        }
    }
}
