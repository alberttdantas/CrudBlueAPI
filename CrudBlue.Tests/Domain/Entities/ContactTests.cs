
using CrudBlue.Domain.Entities;

namespace CrudBlue.Tests.Domain.Entities;

public class ContactTests
{
    [Fact]
    public void NewContact_ShouldBeActiveByDefault()
    {
        // Arrange & Act
        var contact = new Contact();

        // Assert
        Assert.True(contact.Active, "Um novo contato deve ser ativo por padrão.");
    }

    [Fact]
    public void SetActive_ShouldChangeActiveState()
    {
        // Arrange
        var contact = new Contact();

        // Act
        contact.Active = false;

        // Assert
        Assert.False(contact.Active, "A propriedade Active deve ser alterável.");
    }
}
