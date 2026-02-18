using CNA.Application.Catalog.Queries.ProductVariant;
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
            var productVariants = await _repository.GetByProductId(query.ProductId);

            return productVariants.Select(_ => new ProductVariantResponse(
                _.Id, 
                _.Sku, 
                _.Description, 
                _.Brand, 
                _.Price,
                _.Stock.Quantity, 
                _.Attributes.ToDictionary(
                    x => x.Name,
                    x => x.Value
                    )
                )
            ).ToList();
        }
    }
}
