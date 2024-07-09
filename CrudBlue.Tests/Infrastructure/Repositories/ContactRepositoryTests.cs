using Moq;
using CrudBlue.Domain.Entities;
using CrudBlue.Infrastructure.Data;
using CrudBlue.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CrudBlue.Tests.Infrastructure.Repositories
{
    public class ContactRepositoryTests
    {


        private readonly Mock<DbSet<Contact>> _mockSet;
        private readonly Mock<AgendaDbContext> _mockContext;
        private readonly ContactRepository _repository;
        private readonly List<Contact> contacts;

        public ContactRepositoryTests()
        {
            _mockSet = new Mock<DbSet<Contact>>();
            _mockContext = new Mock<AgendaDbContext>();
            contacts = new List<Contact>
        {
            new Contact { Id = 1, Name = "Test1", Email = "test1@test.com", Phone = "1234567890", Active = true },
            new Contact { Id = 2, Name = "Test2", Email = "test2@test.com", Phone = "0987654321", Active = false }
        };

            var contactsQueryable = contacts.AsQueryable();

            _mockSet.As<IAsyncEnumerable<Contact>>()
                .Setup(m => m.GetAsyncEnumerator(default))
                .Returns(new TestAsyncEnumerator<Contact>(contactsQueryable.GetEnumerator()));

            _mockSet.As<IQueryable<Contact>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<Contact>(contactsQueryable.Provider));

            _mockSet.As<IQueryable<Contact>>().Setup(m => m.Provider).Returns(contactsQueryable.Provider);
            _mockSet.As<IQueryable<Contact>>().Setup(m => m.Expression).Returns(contactsQueryable.Expression);
            _mockSet.As<IQueryable<Contact>>().Setup(m => m.ElementType).Returns(contactsQueryable.ElementType);
            _mockSet.As<IQueryable<Contact>>().Setup(m => m.GetEnumerator()).Returns(() => contactsQueryable.GetEnumerator());

            _mockContext.Setup(m => m.Contacts).Returns(_mockSet.Object);
            _repository = new ContactRepository(_mockContext.Object);
        }

        [Fact]
        public async Task CreateContactAsync_ShouldAddContact()
        {
            var contact = new Contact { Id = 1, Name = "Test", Email = "test@test.com", Phone = "1234567890", Active = true };
            await _repository.CreateContactAsync(contact);
            _mockSet.Verify(m => m.AddAsync(contact, default), Times.Once());
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
        }

        [Fact]
        public async Task DeleteContactAsync_ShouldRemoveContact()
        {
            var contact = new Contact { Id = 1, Name = "Test", Email = "test@test.com", Phone = "1234567890", Active = true };
            await _repository.DeleteContactAsync(contact);
            _mockSet.Verify(m => m.Remove(contact), Times.Once());
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllContacts()
        {
            // Arrange
            var expectedContacts = new List<Contact>
    {
        new Contact { Id = 1, Name = "Test1", Email = "test1@test.com", Phone = "1234567890", Active = true },
        new Contact { Id = 2, Name = "Test2", Email = "test2@test.com", Phone = "0987654321", Active = false }
    };

            _mockSet.As<IQueryable<Contact>>().Setup(m => m.Provider).Returns(expectedContacts.AsQueryable().Provider);
            _mockSet.As<IQueryable<Contact>>().Setup(m => m.Expression).Returns(expectedContacts.AsQueryable().Expression);
            _mockSet.As<IQueryable<Contact>>().Setup(m => m.ElementType).Returns(expectedContacts.AsQueryable().ElementType);
            _mockSet.As<IQueryable<Contact>>().Setup(m => m.GetEnumerator()).Returns(() => expectedContacts.GetEnumerator());

            // Act
            var actualContacts = await _repository.GetAllAsync();

            // Assert
            Assert.NotNull(actualContacts);
            Assert.Equal(expectedContacts.Count, actualContacts.Count());
            Assert.Equal(expectedContacts, actualContacts.ToList(), new ContactComparer());
        }

        class ContactComparer : IEqualityComparer<Contact>
        {
            public bool Equals(Contact x, Contact y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                    return false;

                return x.Id == y.Id && x.Name == y.Name && x.Email == y.Email && x.Phone == y.Phone && x.Active == y.Active;
            }

            public int GetHashCode(Contact contact)
            {
                if (ReferenceEquals(contact, null)) return 0;

                int hashContactId = contact.Id.GetHashCode();
                int hashContactName = contact.Name == null ? 0 : contact.Name.GetHashCode();

                return hashContactId ^ hashContactName;
            }
        }



        [Fact]
        public async Task GetByIdAsync_ShouldReturnContactById()
        {
            // Arrange
            var contact = new Contact { Id = 1, Name = "Test", Email = "test@test.com", Phone = "1234567890", Active = true };
            _mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(contact);

            // Act
            var result = await _repository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(contact, result);
        }

        [Fact]
        public async Task InactivateContactAsync_ShouldSetContactInactive()
        {
            // Arrange
            var contact = new Contact { Id = 1, Name = "Test", Email = "test@test.com", Phone = "1234567890", Active = true };
            _mockSet.Setup(m => m.Update(contact)).Callback<Contact>(c => c.Active = false);
            _mockContext.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var success = await _repository.InactivateContactAsync(contact);

            // Assert
            Assert.True(success, "O contato deve ser inativado com sucesso.");
            Assert.False(contact.Active, "O estado ativo do contato deve ser falso após a inativação.");
        }
    }
}
