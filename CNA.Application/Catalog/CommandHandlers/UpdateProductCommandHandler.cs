using CNA.Application.Catalog.Commands;
using CNA.Application.Interfaces;
using MediatR;

namespace CNA.Application.Catalog.CommandHandlers
{
    public class UpdateProductCommandHandler
    : IRequestHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _repository;

        public UpdateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(
            UpdateProductCommand command,
            CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(command.ProductId)
                ?? throw new Exception("Product not found");

            var r = command.Request;

            //product.UpdateDetails(
            //    r.Name,
            //    r.Description,
            //    r.Brand,
            //    r.CategoryId,
            //    r.IsActive
            //);

            await _repository.UpdateAsync(product);
        }
    }
}
