using CNA.Application.Catalog.Commands.Cart;
using CNA.Application.Interfaces;
using MediatR;

namespace CNA.Application.Catalog.CommandHandlers.Cart
{
    public class RemoveCartItemCommandHandler : IRequestHandler<RemoveCartItemCommand>
    {
        private readonly ICartItemRepository _cartItemRepository;

        public RemoveCartItemCommandHandler(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public async Task Handle(RemoveCartItemCommand command, CancellationToken cancellationToken)
        {
            var cartItem = await _cartItemRepository.GetByIdAsync(command.CartItemId)
                ?? throw new Exception("CartItem not found");

            await _cartItemRepository.RemoveCartItemAsync(cartItem);
        }
    }
}
