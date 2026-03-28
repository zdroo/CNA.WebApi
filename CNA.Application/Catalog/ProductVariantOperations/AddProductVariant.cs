using CNA.Application.Interfaces;
using MediatR;

namespace CNA.Application.Catalog.ProductVariantOperations;

public static class AddProductVariant
{
    public record Command(
        Guid ProductId,
        string Sku,
        decimal Price,
        string Description,
        string Brand,
        int Quantity,
        VariantAttributeRequest[] VariantAttributes) : IRequest<Guid>;

    public record VariantAttributeRequest(string Name, string Value);

    public class Handler : IRequestHandler<Command, Guid>
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(Command command, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(command.ProductId)
                          ?? throw new Exception("Product not found");

            var variant = product.AddVariant(
                command.Sku,
                command.Price,
                command.Description,
                command.Brand,
                command.Quantity,
                command.VariantAttributes.Select(a => (a.Name, a.Value))
            );

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return variant.Id;
        }
    }
}