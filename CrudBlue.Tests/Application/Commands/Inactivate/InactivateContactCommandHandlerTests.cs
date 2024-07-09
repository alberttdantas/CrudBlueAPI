using Moq;
using CrudBlue.Application.Commands.Inactivate;
using CrudBlue.Application.Models.ViewModels;
using CrudBlue.Application.Responses;
using CrudBlue.Domain.Entities;
using CrudBlue.Domain.Repositories;
using AutoMapper;

namespace CrudBlue.Tests.Application.Commands.Inactivate
{
    public class InactivateContactCommandHandlerTests
    {
        private readonly Mock<IContactRepository> _mockContactRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly InactivateContactCommandHandler _handler;

        public InactivateContactCommandHandlerTests()
        {
            _mockContactRepository = new Mock<IContactRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new InactivateContactCommandHandler(_mockContactRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ContactExists_ShouldInactivateContact()
        {
            // Arrange
            var contactId = 1;
            var contact = new Contact { Id = contactId, Name = "John Doe", Email = "john.doe@example.com", Phone = "1234567890", Active = true };
            var contactViewModel = new ContactViewModel { Id = contactId, Name = "John Doe", Email = "john.doe@example.com", Phone = "1234567890", Active = false };
            var command = new InactivateContactCommand(contactId);

            _mockContactRepository.Setup(repo => repo.GetByIdAsync(contactId)).ReturnsAsync(contact);
            _mockContactRepository.Setup(repo => repo.InactivateContactAsync(contact)).ReturnsAsync(true);
            _mockMapper.Setup(m => m.Map<ContactViewModel>(contact)).Returns(contactViewModel);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(response.Success);
            Assert.Equal("Contato inativado com sucesso.", response.Message);
            Assert.NotNull(response.Data);
            _mockContactRepository.Verify(repo => repo.InactivateContactAsync(contact), Times.Once);
        }

        [Fact]
        public async Task Handle_ContactDoesNotExist_ShouldReturnError()
        {
            // Arrange
            var contactId = 1;
            var command = new InactivateContactCommand(contactId);

            _mockContactRepository.Setup(repo => repo.GetByIdAsync(contactId)).ReturnsAsync((Contact)null);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(response.Success);
            Assert.Equal("Contato não encontrado.", response.Message);
            Assert.Null(response.Data);
            _mockContactRepository.Verify(repo => repo.InactivateContactAsync(It.IsAny<Contact>()), Times.Never);
        }
    }
}
