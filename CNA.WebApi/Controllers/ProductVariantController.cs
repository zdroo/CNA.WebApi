using CNA.Application.Catalog.Queries;
using CNA.Application.Catalog.Queries.Filters;
using CNA.Contracts.Requests.Filters;
using CNA.Contracts.Responses;
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
        public async Task<IActionResult> GetProductVariantsByProductId(ProductVariantsFilterRequest filter)
        {
            var queryFilter = new ProductVariantsFilter
            {
                ProductId = filter.ProductId,
                Brand = filter.Brand,
                SearchText = filter.SearchText,
                MinPrice = filter.MinPrice,
                MaxPrice = filter.MaxPrice,
                PageSize = filter.PageSize,
            };
            var variants = await _mediator.Send(new GetProductVariantsQuery(queryFilter));

            return Ok(variants);
        }

        [HttpGet]
        public async Task<ActionResult<Guid>> GetProductVariantsById(Guid productVariantId)
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
