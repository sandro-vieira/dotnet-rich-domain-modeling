using Microsoft.EntityFrameworkCore;
using NerdStore.Catalog.Domain;
using NerdStore.Catalog.Domain.Interfaces;
using NerdStore.Core.Data;

namespace NerdStore.Catalog.Data.Repository;

public class ProductRepository : IProductRepository
{
    private readonly CatalogContext _context;
    public ProductRepository(CatalogContext context) => _context = context;
    private bool _disposed;

    public async Task<IEnumerable<Product>> GetAllAsync()
        => await _context.Products.AsNoTracking().ToListAsync();
    public async Task<Product?> GetByIdAsync(Guid id)
        => await _context.Products.AsNoTracking()
        .FirstOrDefaultAsync(p => p.Id == id);
    public async Task<IEnumerable<Category>> GetCategoriesAsync()
        => await _context.Categories.AsNoTracking().ToListAsync();
    public async Task<IEnumerable<Product>> GetByCategoryAsync(Guid categoryId)
        => await _context.Products.AsNoTracking()
            .Include(p => p.Category)
            .Where(c => c.Category.Id == categoryId)
            .ToListAsync();
    public async Task AddAsync(Product entity)
        => await _context.Products.AddAsync(entity);
    public async Task AddCategoryAsync(Category entity)
        => await _context.Categories.AddAsync(entity);
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
