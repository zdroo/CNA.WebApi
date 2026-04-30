using CNA.Application.Catalog.OrderOperations;
using CNA.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers;

[Authorize]
[Route("api/order")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IUserContextService _userContext;
    private readonly IMediator _mediator;

    public OrderController(IUserContextService userContext, IMediator mediator)
    {
        _userContext = userContext;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _mediator.Send(new GetOrders.Query(_userContext.GetUserId()));
        return Ok(orders);
    }

    [HttpGet("{orderId:guid}")]
    public async Task<IActionResult> GetOrderDetails([FromRoute] Guid orderId, CancellationToken cancellationToken)
    {
        var order = await _mediator.Send(new GetOrderDetails.Query(orderId), cancellationToken);
        return Ok(order);
    }

    [HttpPut("{orderId:guid}/cancel")]
    public async Task<IActionResult> CancelOrder([FromRoute] Guid orderId, CancellationToken cancellationToken)
    {
        await _mediator.Send(new CancelOrder.Command(orderId));
        return Ok();
    }
}
