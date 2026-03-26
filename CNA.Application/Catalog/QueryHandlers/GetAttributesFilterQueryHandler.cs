using CNA.Application.Catalog.Queries.ProductVariant;
using CNA.Application.Interfaces;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.QueryHandlers
{
    public class GetAttributesFilterQueryHandler : IRequestHandler<GetAttributesFilterQuery, VariantFiltersResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetAttributesFilterQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<VariantFiltersResponse> Handle(GetAttributesFilterQuery query, CancellationToken cancellationToken = default)
        {
            var variantFilters = await _productRepository.GetVariantFiltersAsync(query.Filter);

            return new VariantFiltersResponse { Attributes = variantFilters };
        }
    }
}
