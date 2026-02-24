using CNA.Application.Catalog.Commands.Cart;
using CNA.Application.Interfaces;
using MediatR;

namespace CNA.Application.Catalog.CommandHandlers.Cart
{
    public class RemoveCartItemCommandHandler : IRequestHandler<RemoveCartItemCommand>
    {
        private readonly ICartRepository _cartRepository;

        public RemoveCartItemCommandHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task Handle(RemoveCartItemCommand command, CancellationToken cancellationToken)
        {
            var cartItem = await _cartRepository.GetByIdAsync(command.CartItemId)
                ?? throw new Exception("CartItem not found");

            await _cartRepository.RemoveCartItemAsync(cartItem);
        }
    }
}
