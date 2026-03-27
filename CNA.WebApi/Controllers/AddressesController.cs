using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers
{
    [Route("api/address")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        [HttpGet("{addressId:guid}")] 
        public async Task<IActionResult> GetAddress(Guid addressId)
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAddresses()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddAddress()
        {
            return Ok();
        }

        [HttpPut("{addressId:guid}")]
        public async Task<IActionResult> UpdateAddress(Guid addressId)
        {
            return Ok();
        }

        [HttpDelete("{addressId:guid}")]
        public async Task<IActionResult> DeleteAddress(Guid addressId)
        {
            return NoContent();
        }
    }
}
