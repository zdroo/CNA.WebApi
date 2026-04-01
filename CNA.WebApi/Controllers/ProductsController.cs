using CNA.Application.Catalog.AttributesOperations;
using CNA.Application.Catalog.ProductOperations;
using CNA.Application.Catalog.ProductVariantOperations;
using CNA.Contracts.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductResponse>>> GetProducts(
            [FromQuery] GetProducts.Query request,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                request,
                cancellationToken
            );

            return Ok(result);
        }

        [HttpGet("{productId:guid}")]
        public async Task<ActionResult<ProductResponse>> GetById(
            [FromRoute] Guid productId,
            CancellationToken cancellationToken)
        {
            var product = await _mediator.Send(
                new GetProductById.Query(productId),
                cancellationToken);

            return Ok(product);
        }

        [HttpGet("variants-filtered")]
        public async Task<IActionResult> GetFiltered(
            [FromQuery] GetProductVariants.Query request,
            CancellationToken cancellationToken)
        {
            var variants = await _mediator.Send(
                request,
                cancellationToken);

            return Ok(variants);
        }

        [HttpGet("{productSlug}/variants")]
        public async Task<IActionResult> GetVariants(
            [FromRoute] string productSlug,
            CancellationToken cancellationToken)
        {
            var request = new GetProductVariants.Query { ProductSlug = productSlug };

            var variants = await _mediator.Send(
                request,
                cancellationToken);

            return Ok(variants);
        }

        [HttpGet("get-attributes")]
        public async Task<IActionResult> GetAttributes(
            [FromQuery] GetAttributesFilter.Query request,
            CancellationToken cancellationToken)
        {
            var attributes = await _mediator.Send(
                request,
                cancellationToken);

            return Ok(attributes);
        }

        [HttpGet("{productSlug}/{variantSlug}")]
        public async Task<ActionResult<Guid>> GetProductVariantById(Guid productVariantId)
        {
            throw new NotImplementedException();
        }


        //GET /api/products? category = haine              → lista produse
        //GET /api/products/{productSlug}/variants      → lista variante
        //GET /api/products/{productSlug}/{ variantSlug} → detalii varianta
    }
}
