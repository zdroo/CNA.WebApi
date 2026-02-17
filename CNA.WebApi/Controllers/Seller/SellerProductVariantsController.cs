using CNA.Application.Catalog.Commands.ProductVariants;
using CNA.Contracts.Requests.ProductVariants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers.Seller
{
    [Route("api/[controller]")]
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
        Guid productId,
        CreateProductVariantRequest request,
        CancellationToken cancellationToken)
        {
            var variantId = await _mediator.Send(
                new AddProductVariantCommand(productId, request),
                cancellationToken);

            return Ok(variantId);
        }

        [HttpPut("{variantId:guid}")]
        public async Task<IActionResult> UpdateVariant(
            Guid productId,
            Guid variantId,
            UpdateProductVariantRequest request,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(
                new UpdateProductVariantCommand(productId, variantId, request),
                cancellationToken);

            return NoContent();
        }

        [HttpDelete("{variantId:guid}")]
        public async Task<IActionResult> DeleteVariant(
            Guid productId,
            Guid variantId,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(
                new DeleteProductVariantCommand(productId, variantId),
                cancellationToken);

            return NoContent();
        }
    }
}
