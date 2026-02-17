using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.Queries
{
    public record GetProductVariantsByProductIdQuery(Guid ProductId) : IRequest<List<ProductVariantResponse?>>;
}
