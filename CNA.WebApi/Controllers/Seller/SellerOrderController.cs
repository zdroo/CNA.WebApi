using MediatR;
using Microsoft.AspNetCore.Mvc;
//using AppOrderStatus = CNA.Application.Catalog.Queries.Filters.Models.OrderStatus;

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
        public async Task<IActionResult> GetOrders()
        {
            // TODO Command and handler
            //var newFilter = new OrdersFilter(
            //    filter.UserId,
            //    filter.OrderId,
            //    (OrderStatus)filter.OrderStatus,
            //    filter.IsPaid,
            //    filter.MinCost,
            //    filter.MaxCost);

            return Ok();
        }
    }
}
