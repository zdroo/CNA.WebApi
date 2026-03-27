using CNA.Application.Catalog.Commands.Products;
using CNA.Application.Interfaces;
using CNA.Domain.Catalog;
using CNA.Domain.Catalog.Entities;
using MediatR;

namespace CNA.Application.Catalog.CommandHandlers.Products
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IProductRepository _repository;

        public CreateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateProductCommand command, CancellationToken cancellationToken)
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
