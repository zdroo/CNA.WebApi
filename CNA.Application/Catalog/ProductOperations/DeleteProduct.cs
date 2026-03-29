using MediatR;
using CNA.Application.Interfaces;
using CNA.Domain.Exceptions;

namespace CNA.Application.Catalog.ProductOperations;

public static class DeleteProduct
{
    public record Command(Guid ProductId) : IRequest;

    public class Handler : IRequestHandler<Command>
    {
        private readonly IProductRepository _repository;

        public Handler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(command.ProductId)
                ?? throw new ProductNotFoundException(command.ProductId);

            await _repository.DeleteAsync(product);
        }
    }
}