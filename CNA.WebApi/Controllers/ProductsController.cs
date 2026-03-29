using CNA.Application.Catalog.ProductOperations;
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
    }
}
