using jwt.Models;
using jwt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jwt.Controllers
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

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.registerAsync(registerModel);

            if (!result.IsAuthentecated)
            {
                return BadRequest(result.Message);

            }

            // return Ok(result);
            return Ok(new { Token = result.token, ExpireOn = result.Expireson });
        }


        [HttpPost("Token")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel tokenRequestModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.GetTokenAsync(tokenRequestModel);

            if (!result.IsAuthentecated)
            {
                return BadRequest(result.Message);

            }

            // return Ok(result);
            return Ok(new { Token = result.token, ExpireOn = result.Expireson });
        }

        [Authorize(Roles ="Admin")]
        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel addRoleModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(addRoleModel);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);


            return Ok(addRoleModel);

        }

    }
}
