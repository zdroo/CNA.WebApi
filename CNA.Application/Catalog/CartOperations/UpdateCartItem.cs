using MediatR;
using CNA.Application.Interfaces;
using CNA.Application.Services;
using CNA.Domain.Exceptions;

namespace CNA.Application.Catalog.CartOperations;

public static class UpdateCartItem
{
    public record Command(Guid? UserId, Guid? SessionId, Guid CartItemId, int Quantity = 1) : IRequest;

    public class Handler : IRequestHandler<Command>
    {
        private readonly ICartService _cartService;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ICartService cartService, IUnitOfWork unitOfWork)
        {
            _cartService = cartService;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var cart = await _cartService.GetOrCreateCartAsync(command.UserId, command.SessionId, cancellationToken);

            var cartItem = cart.Items.FirstOrDefault(i => i.Id == command.CartItemId)
                ?? throw new CartItemNotFoundException(command.CartItemId);

            cartItem.SetQuantity(command.Quantity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
