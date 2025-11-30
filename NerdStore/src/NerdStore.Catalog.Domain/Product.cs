using NerdStore.Catalog.Domain.ValueObjects;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain;

public class Product : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool Active { get; private set; }
    public decimal Price { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string Image { get; private set; }
    public int StockQuantity { get; private set; }
    public int StockMinQuantity { get; private set; }

    /// <summary>
    /// Entity Framework
    /// </summary>
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; }
    public Dimensions Dimensions { get; private set; }

    public Product(
        string name,
        string description,
        bool active,
        decimal price,
        Guid categoryId,
        Dimensions dimensions,
        string image)
    {
        Name = name;
        Description = description;
        Active = active;
        Price = price;
        CategoryId = categoryId;
        Dimensions = dimensions;
        Image = image;

        Validate();
    }

    public void Activate() => Active = true;

    public void Deactivate() => Active = false;

    public void ChangeCategory(Category category)
    {
        Category = category;
        CategoryId = category.Id;
    }

    public void ChangeDescription(string description)
    {
        Validations.IfEmpty(description, "The product description cannot be empty");
        Description = description;
    }

    public void DeductStock(int quantity)
    {
        if (quantity < 0)
        {
            quantity *= -1;
        }

        Validations.IfFalse(HasStock(quantity), "Insufficient stock");
        StockQuantity -= quantity;
    }

    public void ReplenishStock(int quantity) => StockQuantity += quantity;

    public bool HasStock(int quantity) => StockQuantity > quantity;

    public void SetStockMinQuantity(int quantity)
    {
        Validations.MinOrEqual(quantity, 0, "The minimum stock quantity must be greater or equal to zero");
        StockMinQuantity = quantity;
    }

    public override void Validate()
    {
        Validations.IfEmpty(Name, "The product name cannot be empty");
        Validations.IfEmpty(Description, "The product description cannot be empty");
        Validations.IfEqual(CategoryId, Guid.Empty, "The categoryId cannot be empty");
        Validations.MinOrEqual(Price, 0, "The product value cannot be less or equal to 0");
        Validations.IfEmpty(Image, "The product image cannot be empty");
    }
}
