using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain.Events;

public class ProductLowerStockEvent :DomainEvent
{
    public int Quantity { get; private set; }
    public ProductLowerStockEvent(Guid aggregateId, int quantity)
        : base(aggregateId) => Quantity = quantity;
}
