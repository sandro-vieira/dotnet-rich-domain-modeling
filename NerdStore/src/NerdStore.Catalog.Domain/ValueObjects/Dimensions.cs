using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain.ValueObjects;

public record Dimensions
{
    public int Height { get; }
    public int Width { get; }
    public int Depth { get; }

    public Dimensions(int height, int width, int depth)
    {
        Validations.MinOrEqual(height, 0, "Height must be greater than zero");
        Validations.MinOrEqual(width, 0, "Width must be greater than zero");
        Validations.MinOrEqual(depth, 0, "Depth must be greater than zero");

        Height = height;
        Width = width;
        Depth = depth;
    }

    public int Volume() => Height * Width * Depth;

    public override string ToString() => $"HxWxD: {Height} x {Width} x {Depth}";
}
