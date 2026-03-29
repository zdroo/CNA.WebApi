using CNA.Application.Interfaces;
using CNA.Domain.Exceptions;
using MediatR;

namespace CNA.Application.Catalog.ProductVariantOperations;

public static class UpdateProductVariant
{
    public record Command(
        Guid ProductId,
        Guid VariantId,
        string Sku,
        string Name,
        decimal Price,
        int Quantity,
        VariantAttributeRequest[] VariantAttributes,
        bool IsActive) : IRequest;

    public record VariantAttributeRequest(string Name, string Value);

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

            product.UpdateVariant(
                command.VariantId,
                command.Sku,
                command.Name,
                command.Price,
                command.Quantity,
                command.VariantAttributes.Select(a => (a.Name, a.Value)),
                command.IsActive
            );

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}