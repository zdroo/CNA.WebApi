using MediatR;

namespace CNA.Application.Catalog.Commands
{
    public record DeleteProductVariantCommand(
    Guid ProductId,
    Guid VariantId)
    : IRequest;
}
