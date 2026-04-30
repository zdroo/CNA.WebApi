using CNA.Application.Catalog.ShippingContactOperations;
using CNA.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers;

[Authorize]
[Route("api/shipping-contacts")]
[ApiController]
public class ShippingContactsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserContextService _userContext;

    public ShippingContactsController(IMediator mediator, IUserContextService userContext)
    {
        _mediator = mediator;
        _userContext = userContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetShippingContacts(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetShippingContacts.Query(_userContext.GetUserId()), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddShippingContact(
        [FromBody] AddShippingContact.Command request,
        CancellationToken cancellationToken)
    {
        var command = request with { UserId = _userContext.GetUserId() };
        var id = await _mediator.Send(command, cancellationToken);
        return Ok(new { shippingContactId = id });
    }
}
