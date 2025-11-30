using NerdStore.Catalog.Application.Dtos;

namespace NerdStore.Catalog.Application.Services;

public interface IProductAppService : IDisposable
{
    Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<ProductDto>> GetByCategoryAsync(int categoryId, CancellationToken cancellationToken);
    Task<IEnumerable<CategoryDto>> GetCategoriesAsync(CancellationToken cancellationToken);

    Task AddAsync(ProductDto productDto, CancellationToken cancellationToken);
    Task UpdateAsync(ProductDto productDto, CancellationToken cancellationToken);

    Task<ProductDto> DeductStockAsync(Guid productId, int quantity, CancellationToken cancellationToken);
    Task<ProductDto> ReplenishStockAsync(Guid productId, int quantity, CancellationToken cancellationToken);
}
