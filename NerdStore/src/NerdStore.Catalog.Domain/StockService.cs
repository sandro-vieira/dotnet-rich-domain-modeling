using NerdStore.Catalog.Domain.Interfaces;

namespace NerdStore.Catalog.Domain;

public class StockService(IProductRepository productRepository) : IStockService
{
    private bool _disposed;
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<bool> DeductStockAsync(Guid productId, int quantity)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product is null)
        {
            return false;
        }

        if (!product.HasStock(quantity))
        {
            return false;
        }

        product.DeductStock(quantity);
        _productRepository.Update(product);
        return await _productRepository.UnitOfWork.CommitAsync();
    }

    public async Task<bool> ReplenishStockAsync(Guid productId, int quantity)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product is null)
        {
            return false;
        }
    
        product.ReplenishStock(quantity);
        _productRepository.Update(product);
        return await _productRepository.UnitOfWork.CommitAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _productRepository.Dispose();
            }

            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
