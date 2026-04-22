using CNA.Application.Catalog.ProductVariantOperations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers
{
    [Route("api/variants")]
    [ApiController]
    public class ProductVariantController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductVariantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetFiltered(
            [FromQuery] GetProductVariants.Query request,
            CancellationToken cancellationToken)
        {
            var variants = await _mediator.Send(request, cancellationToken);
            return Ok(variants);
        }

        [HttpGet("{variantId:guid}")]
        public async Task<ActionResult<Guid>> GetById(
            [FromRoute] Guid variantId,
            CancellationToken cancellationToken)
        {
            var variant = await _mediator.Send(
                new GetProductVariantById.Query(variantId),
                cancellationToken);

            return Ok(variant);
        }
    }
}
