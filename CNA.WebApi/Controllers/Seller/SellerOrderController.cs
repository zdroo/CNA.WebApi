using CNA.Application.Catalog.OrderOperations;
using CNA.Domain.Catalog.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers.Seller
{
    [Route("api/seller/order")]
    [ApiController]
    public class SellerOrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SellerOrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders(
            [FromQuery] Guid? userId,
            [FromQuery] Guid? orderId,
            [FromQuery] OrderStatus? orderStatus,
            [FromQuery] bool isPaid,
            [FromQuery] decimal? minCost,
            [FromQuery] decimal? maxCost,
            CancellationToken cancellationToken)
        {
            var orders = await _mediator.Send(
                new GetSellerOrders.Query(userId, orderId, orderStatus, isPaid, minCost, maxCost),
                cancellationToken);

            return Ok(orders);
        }
    }
}
