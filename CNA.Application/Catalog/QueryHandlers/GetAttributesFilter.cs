using CNA.Application.Catalog.Filters;
using CNA.Application.Interfaces;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.ProductVariants;

public static class GetAttributesFilter
{
    public record Query(AttributesFilter Filter) : IRequest<VariantFiltersResponse>;

    public class Handler : IRequestHandler<Query, VariantFiltersResponse>
    {
        private readonly IProductRepository _productRepository;

        public Handler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<VariantFiltersResponse> Handle(Query query, CancellationToken cancellationToken)
        {
            var variantFilters = await _productRepository.GetVariantFiltersAsync(query.Filter);

            return new VariantFiltersResponse { Attributes = variantFilters };
        }
    }
}