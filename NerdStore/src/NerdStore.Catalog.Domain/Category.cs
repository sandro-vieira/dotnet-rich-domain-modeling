using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain;

public class Category : Entity
{
    public string Name { get; private set; }
    public int Code { get; private set; }

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
        Validations.IfEqual(0, Code, "The category code cannot be zero");
    }
}