using NerdStore.Catalog.Domain.ValueObjects;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain.Tests;

public class ProductTests
{
    [Theory]
    [InlineData("", "Description", 100, "11111111-1111-1111-1111-111111111111", "Image", "The product name cannot be empty")]
    [InlineData(" ", "Description", 100, "11111111-1111-1111-1111-111111111111", "Image", "The product name cannot be empty")]
    [InlineData("Name", "", 100, "11111111-1111-1111-1111-111111111111", "Image", "The product description cannot be empty")]
    [InlineData("Name", " ", 100, "11111111-1111-1111-1111-111111111111", "Image", "The product description cannot be empty")]
    [InlineData("Name", "Description", 0, "11111111-1111-1111-1111-111111111111", "Image", "The product value cannot be less or equal to 0")]
    [InlineData("Name", "Description", -1, "11111111-1111-1111-1111-111111111111", "Image", "The product value cannot be less or equal to 0")]
    [InlineData("Name", "Description", 100, "00000000-0000-0000-0000-000000000000", "Image", "The categoryId cannot be empty")]
    [InlineData("Name", "Description", 100, "11111111-1111-1111-1111-111111111111", "", "The product image cannot be empty")]
    [InlineData("Name", "Description", 100, "11111111-1111-1111-1111-111111111111", " ", "The product image cannot be empty")]
    public void Product_ShouldThrowException_WhenInvalid(
        string name,
        string description,
        decimal value,
        Guid categoryId,
        string image,
        string expectedMessage)
    {
        //Arrage & Act
        var ex = Assert.Throws<DomainException>(() => new Product(
            name,
            description,
            false,
            value,
            categoryId,
            new Dimensions(1, 1, 1),
            DateTime.Now,
            image));

        //Assert
        Assert.Equal(expectedMessage, ex.Message);
    }
}
