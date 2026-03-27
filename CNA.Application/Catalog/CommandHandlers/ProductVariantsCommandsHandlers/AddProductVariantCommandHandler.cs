using CNA.Application.Catalog.Commands.ProductVariants;
using CNA.Application.Interfaces;
using MediatR;

namespace CNA.Application.Catalog.CommandHandlers.ProductVariants
{
    public class AddProductVariantCommandHandler
        : IRequestHandler<AddProductVariantCommand, Guid>
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public AddProductVariantCommandHandler(
            IProductRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
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
                r.Description,
                r.Brand,
                r.Quantity,
                r.VariantAttributes.Select(a => (a.Name, a.Value))
            );

            await _unitOfWork.SaveChangesAsync();

            return variant.Id;
        }
    }
}
