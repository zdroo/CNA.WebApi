using CNA.Domain.Catalog.Entities.Localization;

namespace CNA.Application.Interfaces
{
    public interface ICountryRepository
    {
        Task<List<Country>> GetCountries();
        Task<Country?> GetCountryById(Guid id);
    }
}
