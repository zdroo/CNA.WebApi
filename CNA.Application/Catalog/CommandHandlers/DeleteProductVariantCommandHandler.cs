using CNA.Application.Catalog.Commands;
using CNA.Application.Interfaces;
using MediatR;

namespace CNA.Application.Catalog.CommandHandlers
{
    public class DeleteProductVariantCommandHandler
        : IRequestHandler<DeleteProductVariantCommand>
    {
        private readonly IProductRepository _repository;

        public DeleteProductVariantCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(
            DeleteProductVariantCommand command,
            CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(command.ProductId)
                ?? throw new Exception("Product not found");

            product.RemoveVariant(command.VariantId);

            await _repository.UpdateAsync(product);
        }
    }
}
