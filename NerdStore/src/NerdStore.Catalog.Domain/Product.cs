using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain;

public class Product : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool Active { get; private set; }
    public decimal Value { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string Image { get; private set; }
    public int StockQuantity { get; private set; }

    /// <summary>
    /// Entity Framework
    /// </summary>
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; }

    public Product(
        string name,
        string description,
        bool active,
        decimal value,
        Guid categoryId,
        DateTime createdAt,
        string image)
    {
        Name = name;
        Description = description;
        Active = active;
        Value = value;
        CategoryId = categoryId;
        CreatedAt = createdAt;
        Image = image;
    }

    public void Activate() => Active = true;

    public void Deactivate() => Active = false;

    public void ChangeCategory(Category category)
    { 
        Category = category;
        CategoryId = category.Id;
    }

    public void ChangeDescription(string description) => Description = description;

    public void RemoveFromStock(int quantity)
    {
        if (quantity < 0)
        {
            quantity *= -1;
        }

        StockQuantity -= quantity;
    }

    public void AddStock(int quantity) => StockQuantity += quantity;

    public bool HasStock(int quantity) => StockQuantity > quantity;

    public void Validate()
    {
        // Method intentionally left empty.
    }
}
