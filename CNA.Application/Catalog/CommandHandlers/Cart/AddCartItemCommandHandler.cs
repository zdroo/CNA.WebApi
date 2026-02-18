using CNA.Application.Catalog.Commands.Cart;
using CNA.Application.Interfaces;
using MediatR;

namespace CNA.Application.Catalog.CommandHandlers.Cart
{
    public class AddCartItemCommandHandler : IRequestHandler<AddCartItemCommand>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductVariantRepository _variantRepository;
        private readonly IUserRepository _userRepository;

        public AddCartItemCommandHandler(
            ICartRepository cartRepository,
            IProductVariantRepository variantRepository,
            IUserRepository userRepository)
        {
            _cartRepository = cartRepository;
            _variantRepository = variantRepository;
            _userRepository = userRepository;
        }

        public async Task Handle(AddCartItemCommand command, CancellationToken cancellationToken = default)
        {
            Domain.Catalog.Entities.Cart? cart;
            var variant = await _variantRepository.GetByProductVariantId(command.ProductVariantId)
                ?? throw new ArgumentException("Variant not found");

            cart = await _cartRepository.GetByUserIdAsync(command.UserId);

            if (cart == null)
            {
                var user = await _userRepository.GetByIdAsync(command.UserId);
                if (user == null)
                    throw new ArgumentException("User not found");
                cart = user.GetOrCreateCart();
            }

            cart.AddItem(variant.Id, variant.Price);
        }
    }
}
