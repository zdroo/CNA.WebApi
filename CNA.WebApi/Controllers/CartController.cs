using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        [HttpGet]
        public Task<ActionResult> GetCartItems()
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public Task<ActionResult> AddToCart()
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
