using MediatR;
using CNA.Application.Interfaces;
using CNA.Domain.Exceptions;
using AutoMapper;

namespace CNA.Application.Catalog.CartOperations;

public static class UpdateCartItem
{
    public record Command(Guid UserId, Guid CartItemId, int Quantity = 1)
        : IRequest<CartItemResponse>;

    public record CartItemResponse(
        Guid Id,
        Guid ProductVariantId,
        Guid CartId,
        int Quantity,
        decimal Price,
        decimal Total
    );

    public class Handler : IRequestHandler<Command, CartItemResponse>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(
            ICartRepository cartRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _cartRepository = cartRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CartItemResponse> Handle(Command command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId)
                ?? throw new UserNotFoundException(command.UserId);

            var cart = user.GetOrCreateCart();

            var cartItem = await _cartRepository.GetByIdAsync(command.CartItemId)
                ?? throw new Exception("Item not found");

            cartItem.Increase(command.Quantity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CartItemResponse>(cartItem);
        }
    }
}