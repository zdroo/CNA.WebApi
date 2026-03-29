using AutoMapper;
using CNA.Application.Interfaces;
using CNA.Contracts.Responses;
using MediatR;
using System.Collections.Generic;

namespace CNA.Application.Catalog.CountryOperations;

public static class GetCountries
{
    public record Query() : IRequest<List<CountryResponse>>;

    public class Handler : IRequestHandler<Query, List<CountryResponse>>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public Handler(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<List<CountryResponse>> Handle(Query query, CancellationToken cancellationToken)
        {
            var countries = await _countryRepository.GetCountries();
            return _mapper.Map<List<CountryResponse>>(countries);
        }
    }
}