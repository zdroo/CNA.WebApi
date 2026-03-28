using CNA.Application.Catalog.Queries.CountryQueries;
using CNA.Application.Interfaces;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.QueryHandlers
{
    public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, List<CountryResponse>>
    {
        private readonly ICountryRepository _countryRepository;

        public GetCountriesQueryHandler(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<List<CountryResponse>> Handle(
            GetCountriesQuery query,
            CancellationToken cancellationToken = default)
        {
            var countries = await _countryRepository.GetCountries();

            return countries.Select(_ => new CountryResponse(
                _.Name,
                _.CountryCode,
                _.CurrencyCode,
                _.PhonePrefix,
                _.IsShippingAvailable)).ToList();
        }
    }
}
