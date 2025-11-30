namespace NerdStore.Catalog.Domain;

public interface IProductService : IDisposable
{
    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken);
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetByCategoryAsync(int categoryCode, CancellationToken cancellationToken);
    Task<IEnumerable<Category>> GetCategoriesAsync(CancellationToken cancellationToken);

    Task AddAsync(Product entity, CancellationToken cancellationToken);
    Task UpdateAsync(Product entity, CancellationToken cancellationToken);
}

