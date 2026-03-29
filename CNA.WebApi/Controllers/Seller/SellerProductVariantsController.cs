using CNA.Application.Catalog.ProductVariantOperations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers.Seller
{
    [Route("api/seller/product-variant")]
    [ApiController]
    public class SellerProductVariantsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SellerProductVariantsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> AddVariant(
        [FromBody] AddProductVariant.Command request,
        CancellationToken cancellationToken)
        {
            var variantId = await _mediator.Send(
                request,
                cancellationToken);

            return Ok(variantId);
        }

        [HttpPut("{variantId:guid}")]
        public async Task<IActionResult> UpdateVariant(
            [FromRoute] Guid variantId,
            [FromBody] UpdateProductVariant.Command request,
            CancellationToken cancellationToken)
        {
            request = request with { VariantId = variantId };

            await _mediator.Send(
                request,
                cancellationToken);

            return Ok();
        }

        [HttpDelete("{variantId:guid}")]
        public async Task<IActionResult> DeleteVariant(
            [FromRoute] Guid variantId,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(
                new DeleteProductVariant.Command(variantId),
                cancellationToken);

            return NoContent();
        }
    }
}
