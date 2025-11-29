using NerdStore.Catalog.Domain.Events;
using NerdStore.Catalog.Domain.Interfaces;
using NerdStore.Core.Bus;

namespace NerdStore.Catalog.Domain;

public class StockService(
    IProductRepository productRepository,
    IMediatorHandler bus) : IStockService
{
    private bool _disposed;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IMediatorHandler _bus = bus;

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

        if (product.StockQuantity <= product.StockMinQuantity)
        {
            await _bus.PublishEvent(new ProductLowerStockEvent(product.Id, product.StockQuantity));
        }

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
