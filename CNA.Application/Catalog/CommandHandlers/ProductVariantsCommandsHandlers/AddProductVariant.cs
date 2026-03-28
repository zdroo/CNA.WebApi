using CNA.Application.Interfaces;
using MediatR;

namespace CNA.Application.Catalog.ProductVariants;

public static class AddProductVariant
{
    public record Command(Guid ProductId, ProductVariantRequest Request) : IRequest<Guid>;

    public record ProductVariantRequest(
        string Sku,
        decimal Price,
        string Description,
        string Brand,
        int Quantity,
        VariantAttributeRequest[] VariantAttributes
    );

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

            var r = command.Request;

            var variant = product.AddVariant(
                r.Sku,
                r.Price,
                r.Description,
                r.Brand,
                r.Quantity,
                r.VariantAttributes.Select(a => (a.Name, a.Value))
            );

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return variant.Id;
        }
    }
}