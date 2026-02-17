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
            var userId = _userContext.GetUserId();
            var cart = await _mediator.Send(new GetCartByUserIdQuery(userId));

            return Ok(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(AddProductVariantToCartRequest request)
        {
            var command = new AddCartItemCommand(CurrentUserId, request.productVariantId);
            var cartItemId = await _mediator.Send(command);
            return Ok(cartItemId);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateQuantity(UpdateCartItemRequest request)
        {
            var command = new UpdateCartItemCommand(CurrentUserId, request.CartItemId, request.Quantity);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveFromCart(RemoveCartItemRequest request)
        {
            var command = new RemoveCartItemCommand(CurrentUserId, request.CartItemId);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
