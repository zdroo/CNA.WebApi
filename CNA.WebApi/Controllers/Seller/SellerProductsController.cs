using CNA.Application.Catalog.ProductOperations;
using CNA.Contracts.Requests;
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
            [FromBody] CreateProductRequest request,
            CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(
                new CreateProduct.Command(request),
                cancellationToken);

            return Ok(id);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            UpdateProduct.Command request,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(
                request,
                cancellationToken);

            return Ok(id);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(
            Guid id,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(
                new DeleteProduct.Command(id),
                cancellationToken);

            return NoContent();
        }
    }
}
