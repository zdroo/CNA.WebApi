using CNA.Application.Interfaces;
using CNA.Contracts.Requests;
using CNA.Domain.Catalog.Entities;
using MediatR;

namespace CNA.Application.Catalog.ProductOperations;

public static class CreateProduct
{
    public record Command(CreateProductRequest Request) : IRequest<Guid>;

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

            var product = new Product(
                r.Name,
                r.Description,
                r.CategoryId
            );

            await _repository.AddAsync(product);

            return product.Id;
        }
    }
}