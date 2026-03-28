using MediatR;
using CNA.Application.Interfaces;
using CNA.Domain.Catalog.Entities;

namespace CNA.Application.Catalog.Products;

public static class CreateProduct
{
    public record Command(ProductRequest Request) : IRequest<Guid>;

    public record ProductRequest(string Name, string Description, Guid CategoryId);

    public class Handler : IRequestHandler<Command, Guid>
    {
        private readonly IProductRepository _repository;

        public Handler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(Command command, CancellationToken cancellationToken)
        {
            var r = command.Request;

            var product = new Domain.Catalog.Entities.Product(
                r.Name,
                r.Description,
                r.CategoryId
            );

            await _repository.AddAsync(product);

            return product.Id;
        }
    }
}