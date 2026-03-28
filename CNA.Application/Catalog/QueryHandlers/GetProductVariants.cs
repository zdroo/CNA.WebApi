using CNA.Application.Catalog.Filters;
using CNA.Application.Interfaces;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.ProductVariants;

public static class GetProductVariants
{
    public record Query(ProductVariantsFilter Filter) : IRequest<List<ProductVariantResponse>>;

    public class Handler : IRequestHandler<Query, List<ProductVariantResponse>>
    {
        private readonly IProductRepository _repository;

        public Handler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductVariantResponse>> Handle(Query query, CancellationToken cancellationToken)
        {
            var products = await _repository.GetFiltered(query.Filter);

            // Extrage toate variantele și map-ează-le la response
            //var variants = products
            //    .SelectMany(p => p.Variants)
            //    .Select(v => new ProductVariantResponse(
            //        v.Id,
            //        v.Sku,
            //        v.Description,
            //        v.Brand,
            //        v.Price,
            //        v.Stock.Quantity,
            //        v.Attributes.ToDictionary(a => a.Name, a => a.Value)
            //    )).ToList();

            return new List<ProductVariantResponse>();
        }
    }
}