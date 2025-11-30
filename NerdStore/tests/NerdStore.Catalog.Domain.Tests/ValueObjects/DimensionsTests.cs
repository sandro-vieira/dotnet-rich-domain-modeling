using NerdStore.Catalog.Domain.ValueObjects;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain.Tests.ValueObjects;

public class DimensionsTests
{
    [Theory]
    [InlineData(0, 1, 1, "Height must be greater than zero")]
    [InlineData(-1, 1, 1, "Height must be greater than zero")]
    [InlineData(1, 0, 1, "Width must be greater than zero")]
    [InlineData(1, -1, 1, "Width must be greater than zero")]
    [InlineData(1, 1, 0, "Depth must be greater than zero")]
    [InlineData(1, 1, -1, "Depth must be greater than zero")]
    public void Dimensions_ShouldThrowException_WhenInvalid(
        int height,
        int width,
        int depth,
        string message)
    {
        //Arrage & Act
        var ex = Assert.Throws<DomainException>(() => new Dimensions(height, width, depth));

        //Assert
        Assert.Equal(message, ex.Message);
    }
}
