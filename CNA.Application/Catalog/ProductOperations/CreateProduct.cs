using CNA.Application.Interfaces;
using CNA.Domain.Catalog.Entities;
using MediatR;

namespace CNA.Application.Catalog.ProductOperations;

public static class CreateProduct
{
    public record Command(string Name, string Description, string Brand, Guid CategoryId) : IRequest<Guid>;

    public class Handler : IRequestHandler<Command, Guid>
    {
        private readonly IProductRepository _repository;

        public Handler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(Command command, CancellationToken cancellationToken)
        {
            var product = new Product(
                command.Name,
                command.Description,
                command.CategoryId
            );

            await _repository.AddAsync(product);

            return product.Id;
        }
    }
}