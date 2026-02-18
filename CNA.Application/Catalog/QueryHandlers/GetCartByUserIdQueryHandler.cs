using CNA.Application.Catalog.Queries.Cart;
using CNA.Application.Interfaces;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.QueryHandlers
{
    public class GetCartByUserIdQueryHandler : IRequestHandler<GetCartByUserIdQuery, CartResponse>
    {
        private readonly IUserRepository _userRepository;

        public GetCartByUserIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<CartResponse> Handle(GetCartByUserIdQuery query, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(query.UserId)
                ?? throw new Exception("User not found.");

            var cart = user.GetOrCreateCart();

            var cartResponse = new CartResponse
            (
                cart.UserId,
                cart.GetTotal(),
                cart.Items.Select(_ => new CartItemResponse
                (
                    _.Id,
                    _.ProductVariantId,
                    _.CartId,
                    _.Quantity,
                    _.Price,
                    _.Total
                )).ToList()
            );

            return cartResponse;
        }
    }
}
