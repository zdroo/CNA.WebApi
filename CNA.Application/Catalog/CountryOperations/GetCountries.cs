using CNA.Application.Interfaces;
using CNA.Contracts.Responses;
using MediatR;

namespace CNA.Application.Catalog.CountryOperations;

public static class GetCountries
{
    public record Query() : IRequest<List<CountryResponse>>;

    public class Handler : IRequestHandler<Query, List<CountryResponse>>
    {
        private readonly ICountryRepository _countryRepository;

        public Handler(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<List<CountryResponse>> Handle(Query query, CancellationToken cancellationToken)
        {
            var countries = await _countryRepository.GetCountries();

            return countries
                .Select(c => new CountryResponse(
                    c.Name,
                    c.CountryCode,
                    c.CurrencyCode,
                    c.PhonePrefix,
                    c.IsShippingAvailable))
                .ToList();
        }
    }
}