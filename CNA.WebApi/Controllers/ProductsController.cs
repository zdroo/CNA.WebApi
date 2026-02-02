using CNA.Application.Catalog.Commands;
using CNA.Application.Catalog.Queries;
using CNA.Contracts.Requests;
using CNA.Contracts.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductResponse>>> GetProducts()
        {
            var result = await _mediator.Send(new GetProductsQuery());
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductResponse>> GetById(Guid id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            return product is null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateProductRequest request)
        {
            var id = await _mediator.Send(new CreateProductCommand(request));
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }


        [HttpDelete]
        public async Task<ActionResult<Guid>> DeleteProduct(Guid productId)
        {
            throw new NotImplementedException();
        }
    }
}
