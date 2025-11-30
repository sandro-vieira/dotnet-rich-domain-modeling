using NerdStore.Core.Data;

namespace NerdStore.Catalog.Domain;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetByCategoryAsync(int categoryCode, CancellationToken cancellationToken);
    Task<IEnumerable<Category>> GetCategoriesAsync(CancellationToken cancellationToken);

    Task AddCategoryAsync(Category entity, CancellationToken cancellationToken);
    void Update(Category entity);
}
