using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        [HttpGet]
        public Task<IActionResult> GetCart(Guid currentUserId) //poate facem o modificare eleganta
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public Task<ActionResult> AddToCart()
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public Task<ActionResult> UpdateQuantity(Guid productVariantId)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public Task<ActionResult> RemoveFromCart()
        {
            throw new NotImplementedException();
        }
    }
}
