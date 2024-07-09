
using CrudBlue.Application.Models.ViewModels;
using CrudBlue.Application.Responses;
using MediatR;

namespace CrudBlue.Application.Queries.GetAll;

public class GetAllContactsQuery : IRequest<ServiceResponse<List<ContactViewModel>>>
{
    public GetAllContactsQuery()
    {
        
    }
}
