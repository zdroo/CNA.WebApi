using CNA.Application.Catalog.OrderOperations;
using CNA.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private Guid CurrentUserId => _userContext.GetUserId();

        private readonly IUserContextService _userContext;
        private readonly IMediator _mediator;

        public OrderController(
            IUserContextService userContext,
            IMediator mediator)
        {
            _userContext = userContext;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _mediator.Send(new GetOrders.Query(CurrentUserId));
            return Ok(orders);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderDetails([FromQuery] GetOrderDetails.Query request)
        {
            var order = await _mediator.Send(request);
            return Ok(order);
        }

        [HttpPut]
        public async Task<IActionResult> CancelOrder(CancelOrder.Command request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
