
using CrudBlue.Application.Models.ViewModels;
using CrudBlue.Application.Responses;
using CrudBlue.Domain.Entities;
using MediatR;

namespace CrudBlue.Application.Commands.Delete;

public class DeleteContactCommand : IRequest<ServiceResponse<ContactViewModel>>
{
    public DeleteContactCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
