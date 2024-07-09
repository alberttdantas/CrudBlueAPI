
using CrudBlue.Application.Models.InputModels;
using CrudBlue.Application.Models.ViewModels;
using CrudBlue.Application.Responses;
using MediatR;

namespace CrudBlue.Application.Commands.Create;

public class CreateContactCommand : IRequest<ServiceResponse<ContactViewModel>>
{
    public CreateContactCommand(ContactInputModel contactInput)
    {
        ContactInput = contactInput;
    }

    public ContactInputModel ContactInput { get; set; }
}
