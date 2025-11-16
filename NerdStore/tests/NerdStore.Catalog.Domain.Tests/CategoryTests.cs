using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain.Tests;

public class CategoryTests
{
    [Theory]
    [InlineData("", 1, "The category name cannot be empty")]
    [InlineData("Name", 0, "The code must be greater than zero")]
    [InlineData("Name", -1, "The code must be greater than zero")]
    public void Category_ShouldThrowException_WhenInvalid(
        string name,
        int code,
        string expectedMessage)
    {
        //Arrage & Act
        var ex = Assert.Throws<DomainException>(() => new Category(name, code));

        //Assert
        Assert.Equal(expectedMessage, ex.Message);
    }
}
