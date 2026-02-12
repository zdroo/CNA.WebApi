using CNA.Domain.Catalog;

namespace CNA.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task AddCategory();
        Task DeleteCategory();
        Task<List<Category>> GetCategoriesAsync();
    }
}
