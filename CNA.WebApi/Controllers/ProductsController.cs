using CNA.Application.Catalog.Queries.Filters;
using CNA.Application.Catalog.Queries.Product;
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
            ProductsFilter filter,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetProductsQuery(filter),
                cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductResponse>> GetById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var product = await _mediator.Send(
                new GetProductByIdQuery(id),
                cancellationToken);

            return product is null ? NotFound() : Ok(product);
        }
    }
}
