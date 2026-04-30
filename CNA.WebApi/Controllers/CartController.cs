using CNA.Application.Catalog.CartOperations;
using CNA.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IUserContextService _userContext;
        private readonly IMediator _mediator;

        public CartController(IUserContextService userContext, IMediator mediator)
        {
            _userContext = userContext;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart(CancellationToken cancellationToken)
        {
            var cart = await _mediator.Send(CartQuery, cancellationToken);
            return Ok(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(
            [FromBody] Guid productVariantId,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new AddCartItem.Command(UserId, SessionId, productVariantId), cancellationToken);
            var cart = await _mediator.Send(CartQuery, cancellationToken);
            return Ok(cart);
        }

        [HttpPut("{cartItemId:guid}")]
        public async Task<IActionResult> UpdateQuantity(
            [FromRoute] Guid cartItemId,
            [FromQuery] int quantity,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdateCartItem.Command(UserId, SessionId, cartItemId, quantity), cancellationToken);
            var cart = await _mediator.Send(CartQuery, cancellationToken);
            return Ok(cart);
        }

        [HttpDelete("{cartItemId:guid}")]
        public async Task<IActionResult> RemoveFromCart(
            [FromRoute] Guid cartItemId,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new RemoveCartItem.Command(UserId, SessionId, cartItemId), cancellationToken);
            var cart = await _mediator.Send(CartQuery, cancellationToken);
            return Ok(cart);
        }

        [HttpDelete]
        public async Task<IActionResult> ClearCart(CancellationToken cancellationToken)
        {
            await _mediator.Send(new ClearCart.Command(UserId, SessionId), cancellationToken);
            return NoContent();
        }

        [Authorize]
        [HttpPost("merge")]
        public async Task<IActionResult> MergeSessionCart(
            [FromQuery] Guid sessionId,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new MergeSessionCart.Command(_userContext.GetUserId(), sessionId), cancellationToken);
            var cart = await _mediator.Send(new GetCartByUserId.Query(_userContext.GetUserId(), null), cancellationToken);
            return Ok(cart);
        }

        [Authorize]
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CartCheckout.Command request, CancellationToken cancellationToken)
        {
            var command = request with { UserId = _userContext.GetUserId() };
            var orderId = await _mediator.Send(command, cancellationToken);
            return Ok(new { orderId });
        }


        private Guid? UserId => _userContext.IsAuthenticated() ? _userContext.GetUserId() : null;
        private Guid? SessionId => !_userContext.IsAuthenticated() ? _userContext.GetSessionId() : null;

        private GetCartByUserId.Query CartQuery => new(UserId, SessionId);
    }
}
