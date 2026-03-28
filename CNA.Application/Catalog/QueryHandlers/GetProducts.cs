using CNA.Application.Interfaces;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.Product;

public static class GetProducts
{
    public record Query() : IRequest<List<ProductResponse>>;

    public class Handler : IRequestHandler<Query, List<ProductResponse>>
    {
        private readonly IProductRepository _repository;

        public Handler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductResponse>> Handle(Query query, CancellationToken cancellationToken)
        {
            var products = await _repository.ListAllAsync();

            return products.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CategoryId = p.CategoryId,
                IsActive = p.IsActive,
                Variants = p.Variants.Select(v => new ProductVariantResponse(
                    v.Id,
                    v.Sku,
                    v.Description,
                    v.Brand,
                    v.Price,
                    v.Stock.Quantity,
                    v.Attributes.ToDictionary(a => a.Name, a => a.Value)
                )).ToList()
            }).ToList();
        }
    }
}