using CNA.Application.Catalog.Commands.Products;
using CNA.Contracts.Requests;
using CNA.Contracts.Requests.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers.Seller
{
    [ApiController]
    [Route("api/seller/products")]
    public class SellerProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SellerProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(
            CreateProductRequest request,
            CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(
                new CreateProductCommand(request),
                cancellationToken);

            return Ok(id);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            UpdateProductRequest request,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(
                new UpdateProductCommand(id, request),
                cancellationToken);

            return Ok(id);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(
            Guid id,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(
                new DeleteProductCommand(id),
                cancellationToken);

            return Ok();
        }
    }
}
