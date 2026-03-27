using CNA.Application.Catalog.Commands.ProductVariants;
using CNA.Application.Interfaces;
using MediatR;

namespace CNA.Application.Catalog.CommandHandlers.ProductVariants
{
    public class DeleteProductVariantCommandHandler
        : IRequestHandler<DeleteProductVariantCommand>
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteProductVariantCommandHandler(
            IProductRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(
            DeleteProductVariantCommand command,
            CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(command.ProductId)
                ?? throw new Exception("Product not found");

            product.RemoveVariant(command.VariantId);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
