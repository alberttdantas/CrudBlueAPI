using Xunit;
using Moq;
using CrudBlue.Application.Commands.Create;
using CrudBlue.Application.Models.ViewModels;
using CrudBlue.Application.Responses;
using CrudBlue.Domain.Entities;
using CrudBlue.Domain.Repositories;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using CrudBlue.Application.Models.InputModels;

namespace CrudBlue.Tests.Application.Commands.Create
{
    public class CreateContactCommandHandlerTests
    {
        private readonly Mock<IContactRepository> _mockContactRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateContactCommandHandler _handler;

        public CreateContactCommandHandlerTests()
        {
            _mockContactRepository = new Mock<IContactRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new CreateContactCommandHandler(_mockContactRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_GivenValidContact_ShouldCreateContact()
        {
            // Arrange
            var contactInput = new ContactInputModel
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Phone = "1234567890",
            };
            var contact = new Contact
            {
                Id = 1,
                Name = "John Doe",
                Email = "john.doe@example.com",
                Phone = "1234567890",
                Active = true
            };
            var contactViewModel = new ContactViewModel
            {
                Id = 1,
                Name = "John Doe",
                Email = "john.doe@example.com",
                Phone = "1234567890",
                Active = true
            };
            var command = new CreateContactCommand(contactInput);

            _mockMapper.Setup(m => m.Map<Contact>(It.IsAny<ContactInputModel>())).Returns(contact);
            _mockContactRepository.Setup(repo => repo.CreateContactAsync(It.IsAny<Contact>())).ReturnsAsync(true);
            _mockMapper.Setup(m => m.Map<ContactViewModel>(It.IsAny<Contact>())).Returns(contactViewModel);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(response.Success);
            Assert.Equal("Contato criado com sucesso.", response.Message);
            Assert.NotNull(response.Data);
            _mockContactRepository.Verify(repo => repo.CreateContactAsync(It.IsAny<Contact>()), Times.Once);
        }

        [Fact]
        public async Task Handle_GivenInvalidContact_ShouldNotCreateContact()
        {
            // Arrange
            var contactInput = new ContactInputModel
            {
                Name = "Jane Doe",
                Email = "jane.doe@example.com",
                Phone = "0987654321",
            };
            var contact = new Contact
            {
                Id = 2,
                Name = "Jane Doe",
                Email = "jane.doe@example.com",
                Phone = "0987654321",
                Active = false
            };
            var command = new CreateContactCommand(contactInput);

            _mockMapper.Setup(m => m.Map<Contact>(It.IsAny<ContactInputModel>())).Returns(contact);
            _mockContactRepository.Setup(repo => repo.CreateContactAsync(It.IsAny<Contact>())).ReturnsAsync(false);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(response.Success);
            Assert.Equal("Falha ao criar o Contato.", response.Message);
            Assert.Null(response.Data);
            _mockContactRepository.Verify(repo => repo.CreateContactAsync(It.IsAny<Contact>()), Times.Once);
        }


    }
}
