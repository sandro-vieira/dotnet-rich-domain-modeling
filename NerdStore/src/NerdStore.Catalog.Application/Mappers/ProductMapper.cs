using NerdStore.Catalog.Application.Dtos;
using NerdStore.Catalog.Domain;
using NerdStore.Catalog.Domain.ValueObjects;

namespace NerdStore.Catalog.Application.Mappers;

public static class ProductMapper
{
    public static Product ToEntity(this ProductDto productDto)
        => new(
            productDto.Name,
            productDto.Description,
            productDto.Active,
            productDto.Price,
            productDto.CategoryId,
            new Dimensions(
                productDto.Height,
                productDto.Width,
                productDto.Depth),
            productDto.Image);

    public static IEnumerable<ProductDto> ToDto(this IEnumerable<Product> products)
        => products.Select(p => p.ToDto());

    public static ProductDto ToDto(this Product product)
        => new()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Active = product.Active,
            Price = product.Price,
            CategoryId = product.CategoryId,
            CreatedAt = product.CreatedAt,
            Image = product.Image,
            StockQuantity = product.StockQuantity,
            StockMinQuantity = product.StockMinQuantity,
            Height = product.Dimensions.Height,
            Width = product.Dimensions.Width,
            Depth = product.Dimensions.Depth
        };
}
