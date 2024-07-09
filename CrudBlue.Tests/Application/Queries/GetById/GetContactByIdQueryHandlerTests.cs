using Moq;
using CrudBlue.Application.Queries.GetById;
using CrudBlue.Application.Models.ViewModels;
using CrudBlue.Domain.Entities;
using CrudBlue.Domain.Repositories;
using AutoMapper;

namespace CrudBlue.Tests.Application.Queries.GetById
{
    public class GetContactByIdQueryHandlerTests
    {
        private readonly Mock<IContactRepository> _mockContactRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetContactByIdQueryHandler _handler;

        public GetContactByIdQueryHandlerTests()
        {
            _mockContactRepository = new Mock<IContactRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetContactByIdQueryHandler(_mockContactRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ContactExists_ShouldReturnContact()
        {
            // Arrange
            var contactId = 1;
            var contact = new Contact { Id = contactId, Name = "John Doe", Email = "john.doe@example.com", Phone = "1234567890", Active = true };
            var contactViewModel = new ContactViewModel { Id = contactId, Name = "John Doe", Email = "john.doe@example.com", Phone = "1234567890", Active = true };
            var query = new GetContactByIdQuery(contactId);

            _mockContactRepository.Setup(repo => repo.GetByIdAsync(contactId)).ReturnsAsync(contact);
            _mockMapper.Setup(m => m.Map<ContactViewModel>(contact)).Returns(contactViewModel);

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(response.Success);
            Assert.Equal("Contato obtido com sucesso.", response.Message);
            Assert.NotNull(response.Data);
            _mockContactRepository.Verify(repo => repo.GetByIdAsync(contactId), Times.Once);
        }

        [Fact]
        public async Task Handle_ContactDoesNotExist_ShouldReturnError()
        {
            // Arrange
            var contactId = 1;
            var query = new GetContactByIdQuery(contactId);

            _mockContactRepository.Setup(repo => repo.GetByIdAsync(contactId)).ReturnsAsync((Contact)null);

            // Act
            var response = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(response.Success);
            Assert.Equal("Contato não encontrado.", response.Message);
            Assert.Null(response.Data);
            _mockContactRepository.Verify(repo => repo.GetByIdAsync(contactId), Times.Once);
        }
    }
}
