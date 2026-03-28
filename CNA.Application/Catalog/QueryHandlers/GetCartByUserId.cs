using CNA.Application.Interfaces;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.Cart;

public static class GetCartByUserId
{
    public record Query(Guid UserId) : IRequest<CartResponse>;

    public class Handler : IRequestHandler<Query, CartResponse>
    {
        private readonly IUserRepository _userRepository;

        public Handler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<CartResponse> Handle(Query query, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(query.UserId)
                       ?? throw new Exception("User not found.");

            var cart = user.GetOrCreateCart();

            var cartResponse = new CartResponse(
                cart.UserId,
                cart.GetTotal(),
                cart.Items.Select(i => new CartItemResponse(
                    i.Id,
                    i.ProductVariantId,
                    i.CartId,
                    i.Quantity,
                    i.Price,
                    i.Total
                )).ToList()
            );

            return cartResponse;
        }
    }
}