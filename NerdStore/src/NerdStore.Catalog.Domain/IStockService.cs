namespace NerdStore.Catalog.Domain;

public interface IStockService : IDisposable
{
    Task<bool> DeductStockAsync(Guid productId, int quantity, CancellationToken cancellationToken);
    Task<bool> ReplenishStockAsync(Guid productId, int quantity, CancellationToken cancellationToken);
}
