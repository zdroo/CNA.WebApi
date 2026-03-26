using CNA.Application.Catalog.Queries.Filters;
using CNA.Application.Catalog.Queries.ProductVariant;
using CNA.Contracts.Requests.Filters;
using CNA.Contracts.Requests.Filters.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductSortBy = CNA.Application.Catalog.Queries.Filters.Models.ProductSortBy;

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
        public async Task<IActionResult> GetProductVariantsByProductId(Guid productId)
        {
            var variants = await _mediator.Send(new GetProductVariantsByProductIdQuery(productId));
            return Ok(variants);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductVariantsFiltered([FromQuery]ProductVariantsFilterRequest filter)
        {
            var queryFilter = new ProductVariantsFilter
            {
                ProductId = filter.ProductId,
                CategoryId = filter.CategoryId,
                Attributes = filter.Attributes,
                Brand = filter.Brand,
                SearchText = filter.SearchText,
                PriceRange = MapFilterPriceRange(filter.PriceRange),
                OnlyActive = filter.OnlyActive,
                OnlyInStock = filter.OnlyInStock,
                Featured = filter.Featured,
                SortBy = (ProductSortBy)filter.SortBy,
                PageSize = filter.PageSize,
                Page = filter.Page,
            };

            var variants = await _mediator.Send(
                new GetProductVariantsQuery(queryFilter));

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

        private Application.Catalog.Queries.Filters.Models.PriceRange? MapFilterPriceRange(PriceRange? requestedPriceRange)
        {
            if (requestedPriceRange is null)
            {
                return null;
            }

            var result = new Application.Catalog.Queries.Filters.Models.PriceRange
            {
                MinPrice = requestedPriceRange.MinPrice,
                MaxPrice = requestedPriceRange.MaxPrice
            };

            return result;
        }
    }
}
