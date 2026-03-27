using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers
{
    [Route("api/shipping-contacts")]
    [ApiController]
    public class ShippingContactsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetShippingContacts()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetShippingContact(Guid shippingContactId)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddShippingContact()
        {
            return Ok();
        }

        [HttpPut("{shippingContactId:guid}")]
        public async Task<IActionResult> UpdateShippingContact()
        {
            return Ok();
        }

        [HttpGet("{shippingContactId:guid}")]
        public async Task<IActionResult> DeleteShippingContact()
        {
            return NoContent();
        }




        //GET /shipping-contacts
        //POST /shipping-contacts
        //PUT /shipping-contacts/{id}
        //DELETE /shipping-contacts/{id}
    }
}
