using CNA.Application.Catalog.Queries;
using CNA.Application.Interfaces;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.QueryHandlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponse?>
    {
        private readonly IProductRepository _repository;

        public GetProductByIdQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductResponse?> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var p = await _repository.GetByIdAsync(query.ProductId);
            if (p == null)
                return null;

            return new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
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
            };
        }
    }
}
