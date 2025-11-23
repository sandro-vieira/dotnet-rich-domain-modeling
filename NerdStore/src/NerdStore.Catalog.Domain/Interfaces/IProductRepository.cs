using NerdStore.Core.Data;

namespace NerdStore.Catalog.Domain.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetByCategoryAsync(Guid categoryId);
    Task<IEnumerable<Category>> GetCategoriesAsync();

    Task AddCategoryAsync(Category entity);
    void Update(Category entity);
}
