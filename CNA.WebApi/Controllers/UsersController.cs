using CNA.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserContextService _userContext;
        private Guid CurrentUserId => _userContext.GetUserId();
        public UsersController(IUserContextService userContext)
        {
            _userContext = userContext;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUserDetails()
        {
            return Ok(CurrentUserId);
        }

        [HttpPut("me")]
        public async Task<IActionResult> UpdateUserDetails()
        {
            return Ok(CurrentUserId);
        }
    }
}
