using CNA.Application.Interfaces;
using CNA.Application.Services;
using MediatR;

namespace CNA.Application.Catalog.CartOperations;

public static class MergeSessionCart
{
    public record Command(Guid UserId, Guid SessionId) : IRequest;

    public class Handler : IRequestHandler<Command>
    {
        private readonly ICartService _cartService;
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ICartService cartService, ICartRepository cartRepository, IUnitOfWork unitOfWork)
        {
            _cartService = cartService;
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var sessionCart = await _cartRepository.GetBySessionIdAsync(command.SessionId);
            if (sessionCart == null || !sessionCart.Items.Any()) return;

            var userCart = await _cartService.GetOrCreateCartAsync(command.UserId, null, cancellationToken);

            foreach (var item in sessionCart.Items)
            {
                var newItem = userCart.AddItem(item.ProductVariantId, item.Price, item.Quantity);
                if (newItem != null)
                    _cartRepository.TrackCartItem(newItem);
            }

            await _cartRepository.DeleteAsync(sessionCart);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
