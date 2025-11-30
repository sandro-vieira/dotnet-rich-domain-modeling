using System.ComponentModel.DataAnnotations;

namespace NerdStore.Catalog.Application.Dtos;

public class ProductDto
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    public Guid CategoryId { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    public string Description { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    [Range(1, 10000, ErrorMessage = "The field {0} must be between {1} and {2}")]
    public int StockQuantity { get; set; }

    public int StockMinQuantity { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    public bool Active { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    public DateTime CreatedAt { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    public string Image { get; set; } = string.Empty;

    [Required(ErrorMessage = "The field {0} is required")]
    [Range(1, 100, ErrorMessage = "The field {0} must be between {1} and {2}")]
    public int Height { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    [Range(1, 100, ErrorMessage = "The field {0} must be between {1} and {2}")]
    public int Width { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    [Range(1, 100, ErrorMessage = "The field {0} must be between {1} and {2}")]
    public int Depth { get; set; }
    public IEnumerable<CategoryDto> Categories { get; set; } = [];
}
