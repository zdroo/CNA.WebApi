using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Add()
        {
            return Ok();
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Idk()
        {
            return Ok();
        }
    }
}
