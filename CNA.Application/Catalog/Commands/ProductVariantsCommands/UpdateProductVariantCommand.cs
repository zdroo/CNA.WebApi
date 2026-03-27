using CNA.Contracts.Requests.ProductVariants;
using MediatR;

namespace CNA.Application.Catalog.Commands.ProductVariants
{
    public record UpdateProductVariantCommand(
    Guid ProductId,
    Guid VariantId,
    UpdateProductVariantRequest Request)
    : IRequest;
}
