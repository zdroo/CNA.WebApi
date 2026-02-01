using CNA.Application.Catalog.Queries;
using CNA.Application.Interfaces;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.QueryHandlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductResponse>>
    {
        private readonly IProductRepository _repository;

        public GetProductsQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductResponse>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await _repository.ListAllAsync();

            return products.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Brand = p.Brand,
                CategoryId = p.CategoryId,
                IsActive = p.IsActive,
                Variants = p.Variants.Select(v => new ProductVariantResponse
                {
                    Id = v.Id,
                    Sku = v.Sku,
                    Price = v.Price,
                    StockQuantity = v.Stock.Quantity,
                    Attributes = v.Attributes.ToDictionary(a => a.Name, a => a.Value)
                }).ToList()
            }).ToList();
        }
    }
}
