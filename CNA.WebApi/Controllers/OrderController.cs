using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpGet]
        public Task<ActionResult> GetOrders()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public Task<ActionResult> GetOrder(Guid orderId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public Task<ActionResult> PlaceOrder()
        {
            throw new NotImplementedException();
        }
    }
}
