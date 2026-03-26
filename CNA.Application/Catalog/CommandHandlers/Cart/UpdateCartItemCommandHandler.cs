using CNA.Application.Catalog.Commands.Cart;
using CNA.Application.Interfaces;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.CommandHandlers.Cart
{
    public class UpdateCartItemCommandHandler : IRequestHandler<UpdateCartItemCommand, CartItemResponse>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UpdateCartItemCommandHandler(ICartRepository cartItemRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _cartRepository = cartItemRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CartItemResponse> Handle(UpdateCartItemCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId)
                ?? throw new Exception("User not found");

            var cart = user.GetOrCreateCart();

            var cartItem = await _cartRepository.GetByIdAsync(command.CartItemId)
                ?? throw new Exception("Item not found");

            cartItem.Increase(command.Quantity);

            return new CartItemResponse(
                cartItem.Id, 
                cartItem.ProductVariantId, 
                cartItem.CartId, 
                cartItem.Quantity, 
                cartItem.Price, 
                cartItem.Total
                );
        }
    }
}
