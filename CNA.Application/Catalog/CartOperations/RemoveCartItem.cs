using CNA.Application.Interfaces;
using CNA.Application.Services;
using CNA.Domain.Exceptions;
using MediatR;

namespace CNA.Application.Catalog.CartOperations;

public static class RemoveCartItem
{
    public record Command(Guid? UserId, Guid? SessionId, Guid CartItemId) : IRequest;

    public class Handler : IRequestHandler<Command>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartService _cartService;

        public Handler(IUnitOfWork unitOfWork, ICartService cartService)
        {
            _unitOfWork = unitOfWork;
            _cartService = cartService;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var cart = await _cartService.GetOrCreateCartAsync(command.UserId, command.SessionId, cancellationToken);
            cart.RemoveItemByCartItemId(command.CartItemId);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
