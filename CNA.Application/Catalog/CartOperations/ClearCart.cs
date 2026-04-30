using MediatR;
using CNA.Application.Interfaces;
using CNA.Application.Services;

namespace CNA.Application.Catalog.CartOperations;

public static class ClearCart
{
    public record Command(Guid? UserId, Guid? SessionId) : IRequest;

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
            var cart = await _cartService.GetCartAsync(command.UserId, command.SessionId, cancellationToken);

            if (cart == null) return;

            cart.Clear();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
