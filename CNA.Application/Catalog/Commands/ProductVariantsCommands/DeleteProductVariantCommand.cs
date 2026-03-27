using MediatR;

namespace CNA.Application.Catalog.Commands.ProductVariants
{
    public record DeleteProductVariantCommand(
    Guid ProductId,
    Guid VariantId)
    : IRequest;
}
