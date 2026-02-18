using CNA.Application.Catalog.Commands.Cart;
using CNA.Application.Interfaces;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.CommandHandlers.Cart
{
    public class UpdateCartItemCommandHandler : IRequestHandler<UpdateCartItemCommand, CartItemResponse>
    {
        private readonly ICartItemRepository _cartItemRepository;

        public UpdateCartItemCommandHandler(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public async Task<CartItemResponse> Handle(UpdateCartItemCommand command, CancellationToken cancellationToken)
        {
            var cartItem = await _cartItemRepository.GetByIdAsync(command.CartItemId)
                ?? throw new Exception("Item not found");

            cartItem.Increase(command.Quantity);

            return new CartItemResponse(
                cartItem.Id, 
                cartItem.ProductVariantId, 
                cartItem.CartId, 
                cartItem.Quantity, 
                cartItem.Price, 
                cartItem.Total
                );
        }
    }
}
