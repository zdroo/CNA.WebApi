using Microsoft.AspNetCore.Mvc;

namespace CNA.WebApi.Controllers
{
    [Route("api/countries")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        [HttpGet("get-countries")]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        [HttpPut("{countryId:guid}")]
        public async Task<IActionResult> Update()
        {
            return Ok();
        }
    }
}
