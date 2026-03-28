using CNA.Application.Catalog.CountryOperations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers
{
    [Route("api/countries")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CountriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-countries")]
        public async Task<IActionResult> Get()
        {
            var countries = await _mediator.Send(new GetCountries.Query());
            return Ok(countries);
        }

        [HttpPut("{countryId:guid}")]
        public async Task<IActionResult> Update()
        {
            return Ok();
        }
    }
}
