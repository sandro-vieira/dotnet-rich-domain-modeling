using Microsoft.EntityFrameworkCore;
using NerdStore.Catalog.Domain;
using NerdStore.Core.Data;

namespace NerdStore.Catalog.Data.Repository;

public class ProductRepository : IProductRepository
{
    private readonly CatalogContext _context;
    public ProductRepository(CatalogContext context) => _context = context;
    private bool _disposed;

    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
        => await _context.Products.AsNoTracking().ToListAsync(cancellationToken);
    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await _context.Products.AsNoTracking()
        .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    public async Task<IEnumerable<Category>> GetCategoriesAsync(CancellationToken cancellationToken)
        => await _context.Categories.AsNoTracking().ToListAsync(cancellationToken);
    public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryCode, CancellationToken cancellationToken)
        => await _context.Products.AsNoTracking()
            .Include(p => p.Category)
            .Where(c => c.Category.Code == categoryCode)
            .ToListAsync(cancellationToken);
    public async Task AddAsync(Product entity, CancellationToken cancellationToken)
        => await _context.Products.AddAsync(entity, cancellationToken);
    public async Task AddCategoryAsync(Category entity, CancellationToken cancellationToken)
        => await _context.Categories.AddAsync(entity, cancellationToken);
    public void Update(Category entity) => _context.Categories.Update(entity);
    public void Update(Product entity) => _context.Products.Update(entity);
    public void Remove(Product entity) => _context.Products.Remove(entity);

    public IUnitOfWork UnitOfWork => _context;

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _context?.Dispose();
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
