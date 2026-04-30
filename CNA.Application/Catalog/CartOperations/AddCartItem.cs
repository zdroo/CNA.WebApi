using CNA.Application.Interfaces;
using CNA.Application.Services;
using CNA.Domain.Exceptions;
using MediatR;

namespace CNA.Application.Catalog.CartOperations;

public static class AddCartItem
{
    public record Command(Guid? UserId, Guid? SessionId, Guid ProductVariantId) : IRequest;

    public class Handler : IRequestHandler<Command>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly ICartService _cartService;
        private readonly ICartRepository _cartRepository;

        public Handler(IUnitOfWork unitOfWork, IProductRepository productRepository, ICartService cartService, ICartRepository cartRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _cartService = cartService;
            _cartRepository = cartRepository;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var variant = await _productRepository.GetVariantById(request.ProductVariantId)
                ?? throw new VariantNotFoundException(request.ProductVariantId);

            var cart = await _cartService.GetOrCreateCartAsync(request.UserId, request.SessionId, cancellationToken);
            var newItem = cart.AddItem(variant.Id, variant.Price);
            if (newItem != null)
                _cartRepository.TrackCartItem(newItem);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
