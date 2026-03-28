using CNA.Application.Interfaces;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.ProductVariantOperations;

public static class GetProductVariantsByProductId
{
    public record Query(Guid ProductId) : IRequest<List<ProductVariantResponse>>;

    public class Handler : IRequestHandler<Query, List<ProductVariantResponse>>
    {
        private readonly IProductRepository _repository;

        public Handler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductVariantResponse>> Handle(Query query, CancellationToken cancellationToken)
        {
            var productVariants = await _repository.GetByProductId(query.ProductId);

            return productVariants.Select(v => new ProductVariantResponse(
                v.Id,
                v.Sku,
                v.Description,
                v.Brand,
                v.Price,
                v.Stock.Quantity,
                v.Attributes.ToDictionary(a => a.Name, a => a.Value)
            )).ToList();
        }
    }
}