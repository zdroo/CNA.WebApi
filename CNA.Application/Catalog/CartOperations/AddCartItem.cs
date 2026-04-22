using CNA.Application.Interfaces;
using CNA.Domain.Exceptions;
using MediatR;

namespace CNA.Application.Catalog.CartOperations;

public static class AddCartItem
{
    public record Command(Guid UserId, Guid ProductVariantId) : IRequest;

    public class Handler : IRequestHandler<Command>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public Handler(
            IUnitOfWork unitOfWork,
            IProductRepository productRepository,
            IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId)
                       ?? throw new UserNotFoundException(request.UserId);

            var cart = user.GetOrCreateCart();

            var variant = await _productRepository
                .GetVariantById(request.ProductVariantId)
                ?? throw new VariantNotFoundException(request.ProductVariantId);

            cart.AddItem(variant.Id, variant.Price);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}

