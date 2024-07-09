
using CrudBlue.Application.Models.ViewModels;
using CrudBlue.Application.Responses;
using CrudBlue.Domain.Entities;
using MediatR;

namespace CrudBlue.Application.Commands.Inactivate;

public class InactivateContactCommand : IRequest<ServiceResponse<ContactViewModel>>
{
    public InactivateContactCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
