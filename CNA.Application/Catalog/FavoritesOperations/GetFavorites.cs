using CNA.Application.Interfaces;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.FavoritesOperations
{
    public static class GetFavorites
    {
        public record Query(Guid? UserId, string? SessionId) : IRequest<List<FavoriteItemResponse>>;

        public class Handler : IRequestHandler<Query, List<FavoriteItemResponse>>
        {
            private readonly IFavoritesRepository _repository;

            public Handler(IFavoritesRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<FavoriteItemResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var items = request.UserId.HasValue
                    ? await _repository.GetAllAsync(request.UserId.Value)
                    : await _repository.GetAllAsync(request.SessionId ?? string.Empty);

                return items.Select(f => new FavoriteItemResponse
                {
                    FavoriteItemId = f.Id,
                    ProductVariantId = f.ProductVariantId,
                    VariantSlug = f.Product.Slug,
                    ProductSlug = f.Product.Product.Slug,
                    Name = f.Product.Name,
                    Brand = f.Product.Brand,
                    Price = f.Product.Price,
                    StockQuantity = f.Product.Stock.Quantity,
                    PrimaryImageUrl = f.Product.Images.FirstOrDefault()?.Url,
                }).ToList();
            }
        }
    }
}
