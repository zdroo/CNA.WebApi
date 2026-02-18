using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.Queries.ProductVariant
{
    public record GetProductVariantsByProductIdQuery(Guid ProductId) : IRequest<List<ProductVariantResponse?>>;
}
