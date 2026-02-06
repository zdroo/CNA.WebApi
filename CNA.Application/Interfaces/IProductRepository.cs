using CNA.Domain.Catalog;

namespace CNA.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(Guid id);
        Task<IReadOnlyList<Product>> ListAllAsync();
        Task AddAsync(Product product);
        Task DeleteAsync(Product product);
        Task UpdateAsync(Product product);

        // optional: cautare dupa categorie, filtrare, etc.
        Task<IReadOnlyList<Product>> ListByCategoryAsync(Guid categoryId);
    }
}
