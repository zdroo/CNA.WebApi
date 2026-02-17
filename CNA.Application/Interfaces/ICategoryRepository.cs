using CNA.Domain.Catalog.Entities;

namespace CNA.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task AddCategoryAsync(Category category);
        Task DeleteCategoryAsync(Category category);
        Task<List<Category>> GetCategoriesAsync();
        Task<Category?> GetByIdAsync(Guid categoryId);
    }
}
