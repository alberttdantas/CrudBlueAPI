
using CrudBlue.Application.Models.ViewModels;
using CrudBlue.Application.Responses;
using CrudBlue.Domain.Entities;
using MediatR;

namespace CrudBlue.Application.Queries.GetById;

public class GetContactByIdQuery : IRequest<ServiceResponse<ContactViewModel>>
{
    public GetContactByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
