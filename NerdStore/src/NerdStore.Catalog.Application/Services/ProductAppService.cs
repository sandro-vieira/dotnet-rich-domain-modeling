using NerdStore.Catalog.Application.Dtos;
using NerdStore.Catalog.Application.Mappers;
using NerdStore.Catalog.Domain;

namespace NerdStore.Catalog.Application.Services;

public class ProductAppService(
    IProductService productService,
    IStockService stockService) : IProductAppService
{
    private bool _disposed;
    private readonly IProductService _productService = productService;
    private readonly IStockService _stockService = stockService;

    public async Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var produtcs = await _productService.GetAllAsync(cancellationToken);
        return produtcs.ToDto();
    }
    public async Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await _productService.GetByIdAsync(id, cancellationToken);
        return product?.ToDto();
    }
    public async Task<IEnumerable<ProductDto>> GetByCategoryAsync(int categoryId, CancellationToken cancellationToken)
    {
        var products = await _productService.GetByCategoryAsync(categoryId, cancellationToken);
        return products.ToDto();
    }
    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync(CancellationToken cancellationToken)
    {
        var categories = await _productService.GetCategoriesAsync(cancellationToken);
        return categories.ToDto();
    }

    public async Task AddAsync(ProductDto productDto, CancellationToken cancellationToken)
        => await _productService.AddAsync(productDto.ToEntity(), cancellationToken);
    public async Task UpdateAsync(ProductDto productDto, CancellationToken cancellationToken)
        => await _productService.UpdateAsync(productDto.ToEntity(), cancellationToken);

    public async Task<ProductDto> DeductStockAsync(Guid productId, int quantity, CancellationToken cancellationToken)
    {
        await _stockService.DeductStockAsync(productId, quantity, cancellationToken);
        return await GetByIdAsync(productId, cancellationToken);
    }
    public async Task<ProductDto> ReplenishStockAsync(Guid productId, int quantity, CancellationToken cancellationToken)
    {
        await _stockService.ReplenishStockAsync(productId, quantity, cancellationToken);
        return await GetByIdAsync(productId, cancellationToken);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _productService?.Dispose();
            _stockService?.Dispose();
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
