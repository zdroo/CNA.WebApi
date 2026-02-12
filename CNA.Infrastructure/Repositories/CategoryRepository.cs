using CNA.Application.Interfaces;
using CNA.Domain.Catalog;
using Microsoft.EntityFrameworkCore;

namespace CNA.Infrastructure.Repositories
{
    internal class CategoryRepository : ICategoryRepository
    {
        private readonly CNADbContext _context;

        public CategoryRepository(CNADbContext context)
        {
            _context = context;
        }

        public async Task AddCategory()
        {

        }
        public async Task DeleteCategory()
        {

        }
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
