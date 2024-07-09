using Moq;
using CrudBlue.Application.Queries.GetAll;
using CrudBlue.Application.Models.ViewModels;
using CrudBlue.Domain.Entities;
using CrudBlue.Domain.Repositories;
using AutoMapper;

namespace CrudBlue.Tests.Application.Queries.GetAll
{
    public class GetAllContactsQueryHandlerTests
    {
        private readonly Mock<IContactRepository> _mockContactRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetAllContactsQueryHandler _handler;

        public GetAllContactsQueryHandlerTests()
        {
            _mockContactRepository = new Mock<IContactRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetAllContactsQueryHandler(_mockContactRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ContactsExist_ShouldReturnContacts()
        {
            // Arrange
            var contacts = new List<Contact>
            {
                new Contact { Id = 1, Name = "John Doe", Email = "john.doe@example.com", Phone = "1234567890", Active = true },
                new Contact { Id = 2, Name = "Jane Doe", Email = "jane.doe@example.com", Phone = "0987654321", Active = false }
            };
            var contactViewModels = new List<ContactViewModel>
            {
                new ContactViewModel { Id = 1, Name = "John Doe", Email = "john.doe@example.com", Phone = "1234567890", Active = true },
                new ContactViewModel { Id = 2, Name = "Jane Doe", Email = "jane.doe@example.com", Phone = "0987654321", Active = false }
            };
            var query = new GetAllContactsQuery();

            _mockContactRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(contacts);
            _mockMapper.Setup(m => m.Map<List<ContactViewModel>>(contacts)).Returns(contactViewModels);

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(response.Success);
            Assert.Equal("Contatos obtidos com sucesso.", response.Message);
            Assert.NotNull(response.Data);
            Assert.Equal(contacts.Count, response.Data.Count);
            _mockContactRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_NoContactsExist_ShouldReturnEmpty()
        {
            // Arrange
            var contacts = new List<Contact>();
            var query = new GetAllContactsQuery();

            _mockContactRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(contacts);
            _mockMapper.Setup(m => m.Map<List<ContactViewModel>>(contacts)).Returns(new List<ContactViewModel>());

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(response.Success);
            Assert.Equal("Nenhum contato encontrado.", response.Message);
            Assert.Null(response.Data);
            _mockContactRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }
    }
}
