using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpGet]
        public Task<ActionResult> GetOrders(Guid userId) //mai elegant trb
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public Task<ActionResult> GetOrderDetails(Guid orderId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public Task<ActionResult> Checkout()
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public Task<ActionResult> CancelOrder()
        {
            throw new NotImplementedException();
        }
    }
}
