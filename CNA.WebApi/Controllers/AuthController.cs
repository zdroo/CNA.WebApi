using CNA.Application.Catalog.AuthOperations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register.Command command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login.Command command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] GetRefreshToken.Command command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("google")]
        public async Task<IActionResult> Google([FromBody] GoogleLogin.Command command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
