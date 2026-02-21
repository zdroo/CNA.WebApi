using CNA.Application.Catalog.Commands.Cart;
using CNA.Application.Catalog.Queries.Cart;
using CNA.Application.Interfaces;
using CNA.Contracts.Requests.Cart;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers
{
    [Route("api/[controller]")]
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
            var cart = await _mediator.Send(new GetCartByUserIdQuery(CurrentUserId));

            return Ok(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddProductVariantToCartRequest request)
        {
            var command = new AddCartItemCommand(CurrentUserId, request.productVariantId);
            await _mediator.Send(command);

            var cartResponse = await _mediator.Send(new GetCartByUserIdQuery(CurrentUserId));

            return Ok(cartResponse);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateQuantity(UpdateCartItemRequest request)
        {
            var command = new UpdateCartItemCommand(request.CartItemId, request.Quantity);
            var cartItem = await _mediator.Send(command);
            return Ok(cartItem);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveFromCart(RemoveCartItemRequest request)
        {
            var command = new RemoveCartItemCommand(request.CartItemId);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] List<Guid> CartItemIds)
        {
            await _mediator.Send(new CartCheckoutCommand(CurrentUserId, CartItemIds));

            return Ok();
        }
    }
}
