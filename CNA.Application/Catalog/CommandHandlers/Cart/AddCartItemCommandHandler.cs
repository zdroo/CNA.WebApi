using CNA.Application.Catalog.Commands.Cart;
using CNA.Application.Interfaces;
using MediatR;

namespace CNA.Application.Catalog.CommandHandlers.Cart
{
    public class AddCartItemCommandHandler : IRequestHandler<AddCartItemCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public AddCartItemCommandHandler(
            IUnitOfWork unitOfWork,
            IProductRepository productRepository,
            IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        public async Task Handle(AddCartItemCommand command, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId)
                        ?? throw new Exception("User not found");

            var cart = user.GetOrCreateCart();

            var variant = await _productRepository.GetByProductVariantId(command.ProductVariantId)
                ?? throw new ArgumentException("Variant not found");

            cart.AddItem(variant.Id, variant.Price);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
