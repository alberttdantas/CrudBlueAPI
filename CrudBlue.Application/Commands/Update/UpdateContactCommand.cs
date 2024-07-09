
using CrudBlue.Application.Models.InputModels;
using CrudBlue.Application.Models.ViewModels;
using CrudBlue.Application.Responses;
using MediatR;

namespace CrudBlue.Application.Commands.Update;

public class UpdateContactCommand : IRequest<ServiceResponse<ContactViewModel>>
{
    public UpdateContactCommand(ContactInputModel contactInput, int id)
    {
        ContactInput = contactInput;
        Id = id;
    }

    public ContactInputModel ContactInput { get; set; }
    public int Id { get; set; }
}
