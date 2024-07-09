using Xunit;
using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CrudBlue.Application.Queries.GetAll;
using CrudBlue.Application.Queries.GetById;
using CrudBlue.Application.Commands.Create;
using CrudBlue.Application.Commands.Update;
using CrudBlue.Application.Commands.Delete;
using CrudBlue.Application.Commands.Inactivate;
using CrudBlue.Application.Models.InputModels;
using CrudBlue.Application.Models.ViewModels;
using CrudBlue.Application.Responses;
using CrudBlue.API.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace CrudBlue.Tests.API.Controllers
{
    public class ContactControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly ContactController _controller;

        public ContactControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new ContactController(_mockMediator.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkObjectResult_WhenContactsExist()
        {
            // Arrange
            var contacts = new List<ContactViewModel>
    {
        new ContactViewModel { Id = 1, Name = "John Doe", Email = "john.doe@example.com", Phone = "1234567890", Active = true }
        // Add more contacts as needed
    };
            var serviceResponse = new ServiceResponse<List<ContactViewModel>>(contacts, "Success", true);
            _mockMediator.Setup(m => m.Send(It.IsAny<GetAllContactsQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ServiceResponse<List<ContactViewModel>>>(okResult.Value);
            Assert.True(returnValue.Success);
            Assert.Equal("Success", returnValue.Message);
            Assert.Equal(contacts, returnValue.Data);
        }


        [Fact]
        public async Task GetById_ShouldReturnOkObjectResult_WhenContactExists()
        {
            // Arrange
            var contactId = 1;
            var contactViewModel = new ContactViewModel { Id = contactId, Name = "John Doe", Email = "john.doe@example.com", Phone = "1234567890", Active = true };
            var serviceResponse = new ServiceResponse<ContactViewModel>(contactViewModel, "Success", true);
            _mockMediator.Setup(m => m.Send(It.IsAny<GetContactByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.GetById(contactId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ServiceResponse<ContactViewModel>>(okResult.Value);
            Assert.True(returnValue.Success);
            Assert.Equal("Success", returnValue.Message);
            Assert.Equal(contactViewModel, returnValue.Data);
        }

        [Fact]
        public async Task Create_ShouldReturnOkObjectResult_WhenContactIsCreated()
        {
            // Arrange
            var contactInput = new ContactInputModel { Name = "New Contact", Email = "new.contact@example.com", Phone = "1234567890" };
            var contactViewModel = new ContactViewModel { Id = 1, Name = "New Contact", Email = "new.contact@example.com", Phone = "1234567890", Active = true };
            var serviceResponse = new ServiceResponse<ContactViewModel>(contactViewModel, "Contato criado com sucesso.", true);
            _mockMediator.Setup(m => m.Send(It.IsAny<CreateContactCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.Create(contactInput);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ServiceResponse<ContactViewModel>>(okResult.Value);
            Assert.True(returnValue.Success);
            Assert.Equal("Contato criado com sucesso.", returnValue.Message);
            Assert.Equal(contactViewModel, returnValue.Data);
        }

        [Fact]
        public async Task Update_ShouldReturnOkObjectResult_WhenContactIsUpdated()
        {
            // Arrange
            var contactId = 1;
            var contactInput = new ContactInputModel { Name = "Updated Contact", Email = "updated.contact@example.com", Phone = "0987654321" };
            var contactViewModel = new ContactViewModel { Id = contactId, Name = "Updated Contact", Email = "updated.contact@example.com", Phone = "0987654321", Active = true };
            var serviceResponse = new ServiceResponse<ContactViewModel>(contactViewModel, "Contato atualizado com sucesso.", true);
            _mockMediator.Setup(m => m.Send(It.IsAny<UpdateContactCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.Update(contactId, contactInput);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ServiceResponse<ContactViewModel>>(okResult.Value);
            Assert.True(returnValue.Success);
            Assert.Equal("Contato atualizado com sucesso.", returnValue.Message);
            Assert.Equal(contactViewModel, returnValue.Data);
        }

        [Fact]
        public async Task Delete_ShouldReturnOkObjectResult_WhenContactIsDeleted()
        {
            // Arrange
            var contactId = 1;
            var serviceResponse = new ServiceResponse<ContactViewModel>(null, "Contato deletado com sucesso.", true);
            _mockMediator.Setup(m => m.Send(It.IsAny<DeleteContactCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.Delete(contactId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ServiceResponse<ContactViewModel>>(okResult.Value);
            Assert.True(returnValue.Success);
            Assert.Equal("Contato deletado com sucesso.", returnValue.Message);
        }

        [Fact]
        public async Task Inactivate_ShouldReturnOkObjectResult_WhenContactIsInactivated()
        {
            // Arrange
            var contactId = 1;
            var serviceResponse = new ServiceResponse<ContactViewModel>(null, "Contato inativado com sucesso.", true);
            _mockMediator.Setup(m => m.Send(It.IsAny<InactivateContactCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.Inactivate(contactId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ServiceResponse<ContactViewModel>>(okResult.Value);
            Assert.True(returnValue.Success);
            Assert.Equal("Contato inativado com sucesso.", returnValue.Message);
        }
    }
}
