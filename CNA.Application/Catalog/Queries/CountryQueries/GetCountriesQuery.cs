using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.Queries.CountryQueries
{
    public record GetCountriesQuery : IRequest<List<CountryResponse>>;
}
