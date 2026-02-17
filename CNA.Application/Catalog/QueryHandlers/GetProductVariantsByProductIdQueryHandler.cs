using CNA.Application.Catalog.Queries;
using CNA.Application.Interfaces;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.QueryHandlers
{
    public class GetProductVariantsByProductIdQueryHandler : IRequestHandler<GetProductVariantsByProductIdQuery, List<ProductVariantResponse>>
    {
        private readonly IProductVariantRepository _repository;

        public GetProductVariantsByProductIdQueryHandler(IProductVariantRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductVariantResponse>> Handle(GetProductVariantsByProductIdQuery query, CancellationToken cancellationToken)
        {
            var product = await _repository.Get(query);

            //return products.Variants.ToList();

            return new List<ProductVariantResponse>();
        }
    }
}
