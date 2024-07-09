using Xunit;
using Moq;
using CrudBlue.Application.Commands.Update;
using CrudBlue.Application.Models.ViewModels;
using CrudBlue.Domain.Entities;
using CrudBlue.Domain.Repositories;
using AutoMapper;
using CrudBlue.Application.Models.InputModels;

namespace CrudBlue.Tests.Application.Commands.Update
{
    public class UpdateContactCommandHandlerTests
    {
        private readonly Mock<IContactRepository> _mockContactRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UpdateContactCommandHandler _handler;

        public UpdateContactCommandHandlerTests()
        {
            _mockContactRepository = new Mock<IContactRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new UpdateContactCommandHandler(_mockContactRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ContactExists_ShouldUpdateContact()
        {
            // Arrange
            var contactId = 1;
            var contactInput = new ContactInputModel
            {
                Name = "Jane Doe",
                Email = "jane.doe@example.com",
                Phone = "0987654321"
            };
            var existingContact = new Contact { Id = contactId, Name = "John Doe", Email = "john.doe@example.com", Phone = "1234567890", Active = true };
            var updatedContactViewModel = new ContactViewModel { Id = contactId, Name = "Jane Doe", Email = "jane.doe@example.com", Phone = "0987654321", Active = true };
            var command = new UpdateContactCommand(contactInput, contactId);

            _mockContactRepository.Setup(repo => repo.GetByIdAsync(contactId)).ReturnsAsync(existingContact);
            _mockContactRepository.Setup(repo => repo.UpdateContactAsync(It.IsAny<Contact>())).ReturnsAsync(true);
            _mockMapper.Setup(m => m.Map(contactInput, existingContact)).Verifiable();
            _mockMapper.Setup(m => m.Map<ContactViewModel>(existingContact)).Returns(updatedContactViewModel);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(response.Success);
            Assert.Equal("Contato atualizado com sucesso.", response.Message);
            Assert.NotNull(response.Data);
            _mockContactRepository.Verify(repo => repo.UpdateContactAsync(It.IsAny<Contact>()), Times.Once);
            _mockMapper.Verify(m => m.Map(contactInput, existingContact), Times.Once);
        }

        [Fact]
        public async Task Handle_ContactDoesNotExist_ShouldReturnError()
        {
            // Arrange
            var contactId = 1;
            var contactInput = new ContactInputModel
            {
                Name = "Jane Doe",
                Email = "jane.doe@example.com",
                Phone = "0987654321"
            };
            var command = new UpdateContactCommand(contactInput, contactId);

            _mockContactRepository.Setup(repo => repo.GetByIdAsync(contactId)).ReturnsAsync((Contact)null);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(response.Success);
            Assert.Equal("Contato não encontrado.", response.Message);
            Assert.Null(response.Data);
            _mockContactRepository.Verify(repo => repo.UpdateContactAsync(It.IsAny<Contact>()), Times.Never);
        }
    }
}
