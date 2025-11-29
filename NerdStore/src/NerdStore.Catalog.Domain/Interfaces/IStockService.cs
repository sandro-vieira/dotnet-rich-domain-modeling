namespace NerdStore.Catalog.Domain.Interfaces;

public interface IStockService : IDisposable
{
    Task<bool> DeductStockAsync(Guid productId, int quantity);
    Task<bool> ReplenishStockAsync(Guid productId, int quantity);
}
