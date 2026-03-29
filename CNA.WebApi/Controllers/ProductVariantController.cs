using CNA.Application.Catalog.AttributesOperations;
using CNA.Application.Catalog.ProductVariantOperations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers
{
    [Route("api/product-variant")]
    [ApiController]
    public class ProductVariantController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductVariantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetVariants(
            [FromQuery] GetProductVariants.Query request,
            CancellationToken cancellationToken)
        {
            var variants = await _mediator.Send(
                request,
                cancellationToken);

            return Ok(variants);
        }

        [HttpGet]
        public async Task<IActionResult> GetAttributes(
            [FromQuery] GetAttributesFilter.Query request,
            CancellationToken cancellationToken)
        {
            var attributes = await _mediator.Send(
                request,
                cancellationToken);

            return Ok(attributes);
        }

        [HttpGet]
        public async Task<ActionResult<Guid>> GetProductVariantById(Guid productVariantId)
        {
            throw new NotImplementedException();
        }
    }
}
