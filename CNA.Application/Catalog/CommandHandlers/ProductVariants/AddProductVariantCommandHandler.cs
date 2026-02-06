using CNA.Application.Catalog.Commands;
using CNA.Application.Interfaces;
using MediatR;

namespace CNA.Application.Catalog.CommandHandlers.ProductVariants
{
    public class AddProductVariantCommandHandler
        : IRequestHandler<AddProductVariantCommand, Guid>
    {
        private readonly IProductRepository _repository;

        public AddProductVariantCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(
            AddProductVariantCommand command,
            CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(command.ProductId)
                ?? throw new Exception("Product not found");

            var r = command.Request;

            var variant = product.AddVariant(
                r.Sku,
                r.Price,
                r.Quantity,
                r.VariantAttributes.Select(a => (a.Name, a.Value))
            );

            await _repository.UpdateAsync(product);

            return variant.Id;
        }
    }
}
