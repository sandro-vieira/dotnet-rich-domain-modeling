using NerdStore.Catalog.Application.Dtos;
using NerdStore.Catalog.Domain;

namespace NerdStore.Catalog.Application.Mappers;

public static class CategoryMapper
{
    public static IEnumerable<CategoryDto> ToDto(this IEnumerable<Category> categories)
        => categories.Select(c => c.ToDto());

    public static CategoryDto ToDto(this Category category)
        => new()
        {
            Id = category.Id,
            Name = category.Name,
            Code = category.Code
        };
}
