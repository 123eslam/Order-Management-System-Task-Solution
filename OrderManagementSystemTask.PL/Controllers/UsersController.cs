using Microsoft.AspNetCore.Mvc;
using OrderManagementSystemTask.BLL.Dtos.AuthenticationDto;
using OrderManagementSystemTask.BLL.Services.AuthenticationServies;
using System.Net;

namespace OrderManagementSystemTask.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IAuthenticationService authenticationService) : ControllerBase
    {
        [HttpPost("Register")]
        [ProducesResponseType(typeof(UserResultDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserResultDto>> Register(RegisterDto registerDto)
        {
            var user = await authenticationService.Register(registerDto);
            return Ok(user);
        }
        [HttpPost("Login")]
        [ProducesResponseType(typeof(UserResultDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserResultDto>> Login(LoginDto loginDto)
        {
            var user = await authenticationService.Login(loginDto);
            return Ok(user);
        }
    }
}
