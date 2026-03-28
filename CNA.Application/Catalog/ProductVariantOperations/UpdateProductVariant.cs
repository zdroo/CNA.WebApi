using CNA.Application.Interfaces;
using MediatR;

namespace CNA.Application.Catalog.ProductVariantOperations;

public static class UpdateProductVariant
{
    public record Command(Guid ProductId, Guid VariantId, UpdateProductVariantRequest Request) : IRequest;

    public record UpdateProductVariantRequest(
        string Sku,
        string Name,
        decimal Price,
        int Quantity,
        VariantAttributeRequest[] VariantAttributes,
        bool IsActive
    );

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

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}