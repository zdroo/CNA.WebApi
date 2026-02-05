using CNA.Contracts.Requests.ProductVariants;
using MediatR;

namespace CNA.Application.Catalog.Commands
{
    public record AddProductVariantCommand(
        Guid ProductId,
        CreateProductVariantRequest Request)
        : IRequest<Guid>;
}
