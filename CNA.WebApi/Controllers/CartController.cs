using CNA.Application.Catalog.CartOperations;
using CNA.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private Guid CurrentUserId => _userContext.GetUserId();

        private readonly IUserContextService _userContext;
        private readonly IMediator _mediator;

        public CartController(IMediator mediator, IUserContextService userContext)
        {
            _mediator = mediator;
            _userContext = userContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var cart = await _mediator.Send(new GetCartByUserId.Query(CurrentUserId));

            return Ok(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] Guid productVariantId)
        {
            var command = new AddCartItem.Command(CurrentUserId, productVariantId);
            await _mediator.Send(command);

            var cartResponse = await _mediator.Send(new GetCartByUserId.Query(CurrentUserId));
            return Ok(cartResponse);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateQuantity([FromBody] UpdateCartItem.Command request)
        {
            var cartItem = await _mediator.Send(request);
            return Ok(cartItem);
        }

        [HttpDelete("{cartItemId:guid}")]
        public async Task<IActionResult> RemoveFromCart(Guid cartItemId)
        {
            await _mediator.Send(new RemoveCartItem.Command(CurrentUserId, cartItemId));

            var cartResponse = await _mediator.Send(new GetCartByUserId.Query(CurrentUserId));
            return Ok(cartResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> ClearCart()
        {
            await _mediator.Send(new ClearCart.Command(CurrentUserId));
            return NoContent();
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CartCheckout.Command request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
