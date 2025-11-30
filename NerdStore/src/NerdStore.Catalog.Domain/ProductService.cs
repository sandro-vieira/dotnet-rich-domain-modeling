namespace NerdStore.Catalog.Domain;

public class ProductService(
    IProductRepository productRepository) : IProductService
{
    private bool _disposed;
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
        => await _productRepository.GetAllAsync(cancellationToken);
    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await _productRepository.GetByIdAsync(id, cancellationToken);
    public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryCode, CancellationToken cancellationToken)
        => await _productRepository.GetByCategoryAsync(categoryCode, cancellationToken);
    public async Task<IEnumerable<Category>> GetCategoriesAsync(CancellationToken cancellationToken)
        => await _productRepository.GetCategoriesAsync(cancellationToken);

    public async Task AddAsync(Product entity, CancellationToken cancellationToken)
    {
        await _productRepository.AddAsync(entity, cancellationToken);
        await _productRepository.UnitOfWork.CommitAsync(cancellationToken);
    }
    public async Task UpdateAsync(Product entity, CancellationToken cancellationToken)
    {
        _productRepository.Update(entity);
        await _productRepository.UnitOfWork.CommitAsync(cancellationToken);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _productRepository?.Dispose();
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
