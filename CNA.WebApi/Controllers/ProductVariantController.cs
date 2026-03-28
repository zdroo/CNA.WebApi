using CNA.Application.Catalog.Filters;
using CNA.Application.Catalog.ProductVariantOperations;
using CNA.Contracts.Requests.Filters;
using CNA.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductSortBy = CNA.Application.Catalog.Filters.Models.ProductSortBy;

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
        public async Task<IActionResult> GetProductVariantsByProductId(GetProductVariantsByProductId.Query request)
        {
            var variants = await _mediator.Send(request);
            return Ok(variants);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductVariantsFiltered(
            [FromQuery] GetProductVariants.Query request,
            CancellationToken cancellationToken)
        {
            var variants = await _mediator.Send(
                request,
                cancellationToken);

            return Ok(variants);
        }

        [HttpGet]
        public async Task<IActionResult> GetAttributes(Guid categoryId, Guid productId)
        {
            var attributes = await _mediator.Send(
                new AttributesFilter(categoryId, productId));

            return Ok(attributes);
        }

        [HttpGet]
        public async Task<ActionResult<Guid>> GetProductVariantById(Guid productVariantId)
        {
            throw new NotImplementedException();
        }
    }
}
