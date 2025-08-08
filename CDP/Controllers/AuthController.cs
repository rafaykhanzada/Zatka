using Core.Constant;
using Core.Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.IService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        //[HttpPost("register")]
        //public async Task<IActionResult> Register([FromBody] UserVM model)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(MessageString.ValidationError);
        //    return Ok(await _authService.Register(model));
        //}

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest(MessageString.ValidationError);
            return Ok(await _authService.Login(model));
        }

        //[HttpPost("refresh-token")]
        //public async Task<IActionResult> RefreshToken([FromBody] TokenRequestVM model)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(MessageString.ValidationError);
        //    return Ok(await _authService.RefreshToken(model));
        //}

        //[HttpPost("update-password")]
        //public async Task<IActionResult> ChangePassword([FromBody] AuthPasswordVM model)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(MessageString.ValidationError);
        //    return Ok(await _authService.ChangePassword(model));
        //}
    }
}
