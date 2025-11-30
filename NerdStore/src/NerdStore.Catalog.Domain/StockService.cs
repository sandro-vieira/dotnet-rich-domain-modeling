using NerdStore.Catalog.Domain.Events;
using NerdStore.Core.Bus;

namespace NerdStore.Catalog.Domain;

public class StockService(
    IProductRepository productRepository,
    IMediatorHandler bus) : IStockService
{
    private bool _disposed;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IMediatorHandler _bus = bus;

    public async Task<bool> DeductStockAsync(Guid productId, int quantity, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(productId, cancellationToken);
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
        return await _productRepository.UnitOfWork.CommitAsync(cancellationToken);
    }

    public async Task<bool> ReplenishStockAsync(Guid productId, int quantity, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(productId, cancellationToken);
        if (product is null)
        {
            return false;
        }
    
        product.ReplenishStock(quantity);
        _productRepository.Update(product);
        return await _productRepository.UnitOfWork.CommitAsync(cancellationToken);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }
        
        if (disposing)
        {
            _productRepository.Dispose();
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
