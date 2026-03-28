using CNA.Application.Interfaces;
using MediatR;

namespace CNA.Application.Catalog.ProductOperations;

public static class UpdateProduct
{
    public record Command(Guid ProductId, UpdateProductRequest Request) : IRequest;

    public record UpdateProductRequest(
        string Name,
        string Description,
        string Brand,
        Guid CategoryId,
        bool IsActive,
        bool IsShippable,
        bool IsDigital,
        bool IsReturnable
    );

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

            product.UpdateProduct(
                r.Name,
                r.Description,
                r.Brand,
                r.CategoryId,
                r.IsActive,
                r.IsShippable,
                r.IsDigital,
                r.IsReturnable
            );

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}