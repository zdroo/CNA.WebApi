using CNA.Application.Interfaces;
using CNA.Domain.Exceptions;
using MediatR;

namespace CNA.Application.Catalog.ProductVariantOperations;

public static class DeleteProductVariant
{
    public record Command(Guid VariantId) : IRequest;

    public class Handler : IRequestHandler<Command>
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var variant = await _repository.GetByProductVariantId(command.VariantId)
                ?? throw new VariantNotFoundException(command.VariantId);

            if (variant.Product is null)
                throw new ArgumentNullException($"Variant '{command.VariantId}' product is null.");

            variant.Product.RemoveVariant(command.VariantId);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}