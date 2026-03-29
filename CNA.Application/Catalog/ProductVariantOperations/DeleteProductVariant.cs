using CNA.Application.Interfaces;
using CNA.Domain.Exceptions;
using MediatR;

namespace CNA.Application.Catalog.ProductVariantOperations;

public static class DeleteProductVariant
{
    public record Command(Guid ProductId, Guid VariantId) : IRequest;

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
            var product = await _repository.GetByIdAsync(command.ProductId)
                          ?? throw new ProductNotFoundException(command.ProductId);

            product.RemoveVariant(command.VariantId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}