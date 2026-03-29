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
            GetProducts.Query request,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                request,
                cancellationToken
            );

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductResponse>> GetById(
            GetProductById.Query request,
            CancellationToken cancellationToken)
        {
            var product = await _mediator.Send(
                request,
                cancellationToken);

            return product is null ? BadRequest() : Ok(product);
        }
    }
}
