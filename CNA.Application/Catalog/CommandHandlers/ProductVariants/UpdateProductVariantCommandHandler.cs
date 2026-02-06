using CNA.Application.Catalog.Commands;
using CNA.Application.Interfaces;
using MediatR;

namespace CNA.Application.Catalog.CommandHandlers.ProductVariants
{
    public class UpdateProductVariantCommandHandler
        : IRequestHandler<UpdateProductVariantCommand>
    {
        private readonly IProductRepository _repository;

        public UpdateProductVariantCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(
            UpdateProductVariantCommand command,
            CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(command.ProductId)
                ?? throw new Exception("Product not found");

            var r = command.Request;

            product.UpdateVariant(
                command.VariantId,
                r.Sku,
                r.Name,
                r.Price,
                r.Quantity,
                r.VariantAttributes.Select(a => (a.Name, a.Value)),
                r.IsActive
            );

            await _repository.UpdateAsync(product);
        }
    }
}
