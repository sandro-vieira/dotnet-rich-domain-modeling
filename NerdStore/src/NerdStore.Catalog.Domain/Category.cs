using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain;

public class Category : Entity
{
    public string Name { get; private set; }
    public int Code { get; private set; }

    // Entity Framework
    public ICollection<Product> Products { get; set; }
    protected Category() { }

    public Category(
        string name,
        int code)
    {
        Name = name;
        Code = code;

        Validate();
    }

    public override string ToString() => $"{Name} - {Code}";

    public override void Validate()
    {
        Validations.IfEmpty(Name, "The category name cannot be empty");
        Validations.MinOrEqual(Code, 0, "The code must be greater than zero");
    }
}