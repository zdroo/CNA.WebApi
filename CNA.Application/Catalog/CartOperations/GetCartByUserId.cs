using CNA.Application.Interfaces;
using CNA.Application.Services;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.CartOperations;

public static class GetCartByUserId
{
    public record Query(Guid? UserId, Guid? SessionId) : IRequest<CartResponse>;

    public class Handler : IRequestHandler<Query, CartResponse>
    {
        private readonly ICartService _cartService;
        private readonly IProductRepository _productRepository;

        public Handler(ICartService cartService, IProductRepository productRepository)
        {
            _cartService = cartService;
            _productRepository = productRepository;
        }

        public async Task<CartResponse> Handle(Query query, CancellationToken cancellationToken)
        {
            var cart = await _cartService.GetCartAsync(query.UserId, query.SessionId, cancellationToken);

            if (cart == null || !cart.Items.Any())
                return new CartResponse(query.UserId ?? Guid.Empty, 0, []);

            var variantIds = cart.Items.Select(i => i.ProductVariantId).ToList();
            var variants = await _productRepository.GetByProductVariantIdsWithDetails(variantIds);
            var variantsDict = variants.ToDictionary(v => v.Id);

            var items = cart.Items.Select(i =>
            {
                variantsDict.TryGetValue(i.ProductVariantId, out var variant);
                return new CartItemResponse(
                    CartItemId: i.Id,
                    ProductVariantId: i.ProductVariantId,
                    CartId: i.CartId,
                    Quantity: i.Quantity,
                    Price: i.Price,
                    Total: i.Total,
                    Name: variant?.Name ?? string.Empty,
                    Brand: variant?.Brand,
                    PrimaryImageUrl: variant?.Images.FirstOrDefault()?.Url,
                    ProductSlug: variant?.Product?.Slug ?? string.Empty,
                    VariantSlug: variant?.Slug ?? string.Empty,
                    StockQuantity: variant?.Stock?.Quantity ?? 0
                );
            }).ToList();

            return new CartResponse(cart.UserId ?? Guid.Empty, cart.GetTotal(), items);
        }
    }
}
