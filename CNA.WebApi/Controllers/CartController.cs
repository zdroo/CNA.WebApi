using CNA.Application.Catalog.CartOperations;
using CNA.Application.Interfaces;
using CNA.Contracts.Requests.Cart;
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
        public async Task<IActionResult> AddToCart([FromBody] AddProductVariantToCartRequest request)
        {
            var command = new AddCartItem.Command(CurrentUserId, request.productVariantId);
            await _mediator.Send(command);

            var cartResponse = await _mediator.Send(new GetCartByUserId.Query(CurrentUserId));

            return Ok(cartResponse);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateQuantity(UpdateCartItemRequest request)
        {
            var command = new UpdateCartItem.Command(CurrentUserId, request.CartItemId, request.Quantity);
            var cartItem = await _mediator.Send(command);
            return Ok(cartItem);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveFromCart(RemoveCartItemRequest request)
        {
            var command = new RemoveCartItem.Command(CurrentUserId, request.CartItemId);
            await _mediator.Send(command);

            var cartResponse = await _mediator.Send(new GetCartByUserId.Query(CurrentUserId));
            return Ok(cartResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> ClearCart()
        {
            var command = new ClearCart.Command(CurrentUserId);
            await _mediator.Send(command);

            var cartResponse = await _mediator.Send(new GetCartByUserId.Query(CurrentUserId));
            return Ok(cartResponse);
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest request)
        {
            await _mediator.Send(new CartCheckout.Command(CurrentUserId, request.ShippingContactId, request.CartItemIds));

            return Ok();
        }
    }
}
