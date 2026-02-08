using CNA.Application.Catalog.Queries;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.QueryHandlers
{
    public class GetProductVariantsQueryHandler : IRequestHandler<GetProductVariantsQuery, List<ProductVariantResponse>>
    {
        public async Task<List<ProductVariantResponse>> Handle(GetProductVariantsQuery query, CancellationToken cancellationToken)
        {
            return new List<ProductVariantResponse>();
        }
    }
}
