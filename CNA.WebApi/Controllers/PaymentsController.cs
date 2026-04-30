using CNA.Application.Interfaces;
using CNA.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace CNA.WebApi.Controllers;

[Authorize]
[Route("api/payments")]
[ApiController]
public class PaymentsController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContextService _userContext;
    private readonly IConfiguration _configuration;

    public PaymentsController(
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork,
        IUserContextService userContext,
        IConfiguration configuration)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
        _configuration = configuration;
    }

    [HttpPost("create-session")]
    public async Task<IActionResult> CreateSession(
        [FromBody] Guid orderId,
        CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(orderId)
            ?? throw new OrderNotFoundException(orderId);

        if (order.UserId != _userContext.GetUserId())
            return Forbid();

        var frontendUrl = _configuration["Frontend:Url"];

        var options = new SessionCreateOptions
        {
            Mode = "payment",
            LineItems = order.Items.Select(item => new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "ron",
                    UnitAmount = (long)(item.Price * 100),
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.ProductName,
                    }
                },
                Quantity = item.Quantity,
            }).ToList(),
            Metadata = new Dictionary<string, string>
            {
                ["orderId"] = orderId.ToString()
            },
            SuccessUrl = $"{frontendUrl}/checkout/confirmation/{orderId}",
            CancelUrl = $"{frontendUrl}/payment/{orderId}?cancelled=true",
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options, cancellationToken: cancellationToken);

        return Ok(new { url = session.Url });
    }

    [AllowAnonymous]
    [HttpPost("webhook")]
    public async Task<IActionResult> Webhook()
    {
        var json = await new StreamReader(Request.Body).ReadToEndAsync();
        var stripeSignature = Request.Headers["Stripe-Signature"];
        var webhookSecret = _configuration["Stripe:WebhookSecret"]!;

        Event stripeEvent;
        try
        {
            stripeEvent = EventUtility.ConstructEvent(json, stripeSignature, webhookSecret);
        }
        catch (StripeException)
        {
            return BadRequest();
        }

        if (stripeEvent.Type == "checkout.session.completed")
        {
            var session = stripeEvent.Data.Object as Session;
            if (session?.Metadata.TryGetValue("orderId", out var orderIdStr) == true
                && Guid.TryParse(orderIdStr, out var orderId))
            {
                var order = await _orderRepository.GetByIdAsync(orderId);
                if (order is not null)
                {
                    order.MarkAsPaid();
                    await _unitOfWork.SaveChangesAsync();
                }
            }
        }

        return Ok();
    }
}
