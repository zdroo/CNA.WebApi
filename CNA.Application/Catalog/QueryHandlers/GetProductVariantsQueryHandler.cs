using CNA.Application.Catalog.Queries.ProductVariant;
using CNA.Application.Interfaces;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.QueryHandlers
{
    public class GetProductVariantsQueryHandler : IRequestHandler<GetProductVariantsQuery, List<ProductVariantResponse>>
    {
        private readonly IProductRepository _repository;

        public GetProductVariantsQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductVariantResponse>> Handle(GetProductVariantsQuery query, CancellationToken cancellationToken)
        {
            var product = await _repository.GetFiltered(query.Filter);

            //return products.Variants.ToList();

            return new List<ProductVariantResponse>();
        }
    }
}
