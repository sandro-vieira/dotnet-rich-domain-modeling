using NerdStore.SimpleMediator.Interfaces;

namespace NerdStore.Catalog.Domain.Events;

public class ProductEventHandler(IProductRepository productRepository)
    : INotificationHandler<ProductLowerStockEvent>
{
    private readonly IProductRepository _productRepository = productRepository;
    public async Task Handle(ProductLowerStockEvent notification, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(notification.AggregateId, cancellationToken);

        if (product is not null)
        {
            return;
        }
    }
}
