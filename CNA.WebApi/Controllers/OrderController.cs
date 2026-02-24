using CNA.Application.Catalog.Commands.Order;
using CNA.Application.Catalog.Queries.Order;
using CNA.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers
{
    [Route("api/[controller]")]
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
            var orders = await _mediator.Send(new GetOrdersQuery(CurrentUserId));
            return Ok(orders);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderDetails(Guid orderId)
        {
            var order = await _mediator.Send(new GetOrderDetailsQuery(orderId));
            return Ok(order);
        }

        [HttpPut]
        public async Task<IActionResult> CancelOrder(Guid orderId)
        {
            await _mediator.Send(new CancelOrderCommand(orderId));
            return Ok();
        }
    }
}
