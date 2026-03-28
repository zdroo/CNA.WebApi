using CNA.Application.Interfaces;
using CNA.Domain.Catalog.Entities.Localization;
using Microsoft.EntityFrameworkCore;

namespace CNA.Infrastructure.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly CNADbContext _context;

        public CountryRepository(CNADbContext context)
        {
            _context = context;
        }

        public async Task<List<Country>> GetCountries()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<Country?> GetCountryById(Guid id)
        {
            return await _context.Countries.FirstOrDefaultAsync(_ => _.Id == id);
        }
    }
}
