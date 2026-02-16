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

        public async Task AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
        }
        public async Task DeleteCategoryAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
        public async Task<Category?> GetByIdAsync(Guid categoryId)
        {
            return await _context.Categories
                .Include(p => p.Products)
                .FirstOrDefaultAsync(c => c.Id == categoryId);
        }
    }
}
