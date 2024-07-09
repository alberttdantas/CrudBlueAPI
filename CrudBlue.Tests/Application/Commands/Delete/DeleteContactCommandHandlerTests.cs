using Xunit;
using Moq;
using CrudBlue.Application.Commands.Delete;
using CrudBlue.Application.Models.ViewModels;
using CrudBlue.Application.Responses;
using CrudBlue.Domain.Entities;
using CrudBlue.Domain.Repositories;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;

namespace CrudBlue.Tests.Application.Commands.Delete
{
    public class DeleteContactCommandHandlerTests
    {
        private readonly Mock<IContactRepository> _mockContactRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly DeleteContactCommandHandler _handler;

        public DeleteContactCommandHandlerTests()
        {
            _mockContactRepository = new Mock<IContactRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new DeleteContactCommandHandler(_mockContactRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ContactExists_ShouldDeleteContact()
        {
            // Arrange
            var contactId = 1;
            var contact = new Contact { Id = contactId, Name = "John Doe", Email = "john.doe@example.com", Phone = "1234567890", Active = true };
            var contactViewModel = new ContactViewModel { Id = contactId, Name = "John Doe", Email = "john.doe@example.com", Phone = "1234567890", Active = true };
            var command = new DeleteContactCommand(contactId); // Corrigido aqui

            _mockContactRepository.Setup(repo => repo.GetByIdAsync(contactId)).ReturnsAsync(contact);
            _mockContactRepository.Setup(repo => repo.DeleteContactAsync(contact)).ReturnsAsync(true);
            _mockMapper.Setup(m => m.Map<ContactViewModel>(contact)).Returns(contactViewModel);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(response.Success);
            Assert.Equal("Contato deletado com sucesso.", response.Message);
            Assert.NotNull(response.Data);
            _mockContactRepository.Verify(repo => repo.DeleteContactAsync(contact), Times.Once);
        }

        [Fact]
        public async Task Handle_ContactDoesNotExist_ShouldReturnError()
        {
            // Arrange
            var contactId = 1;
            var command = new DeleteContactCommand(contactId);

            _mockContactRepository.Setup(repo => repo.GetByIdAsync(contactId)).ReturnsAsync((Contact)null);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(response.Success);
            Assert.Equal("Contato não encontrado.", response.Message);
            Assert.Null(response.Data);
            _mockContactRepository.Verify(repo => repo.DeleteContactAsync(It.IsAny<Contact>()), Times.Never);
        }

    }
}
