using CNA.Contracts.Requests.ProductVariants;
using MediatR;

namespace CNA.Application.Catalog.Commands.ProductVariants
{
    public record AddProductVariantCommand(
        Guid ProductId,
        CreateProductVariantRequest Request)
        : IRequest<Guid>;
}
