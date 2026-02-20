using CNA.Application.Catalog.Queries.Filters;
using CNA.Application.Catalog.Queries.ProductVariant;
using CNA.Contracts.Requests.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductVariantController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductVariantController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetProductVariantsByProductId(Guid productId)
        {
            var variants = await _mediator.Send(new GetProductVariantsByProductIdQuery(productId));
            return Ok(variants);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductVariantsFiltered(Guid productId, ProductVariantsFilterRequest filter)
        {
            var queryFilter = new ProductVariantsFilter
            {
                ProductId = productId,
                Brand = filter.Brand,
                SearchText = filter.SearchText,
                MinPrice = filter.MinPrice,
                MaxPrice = filter.MaxPrice,
                PageSize = filter.PageSize,
            };
            var variants = await _mediator.Send(new GetProductVariantsQuery(productId, queryFilter));

            return Ok(variants);
        }

        [HttpGet]
        public async Task<ActionResult<Guid>> GetProductVariantById(Guid productVariantId)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public async Task<ActionResult<Guid>> DeleteProductVariant(Guid productVariantId)
        {
            throw new NotImplementedException();
        }
    }
}
