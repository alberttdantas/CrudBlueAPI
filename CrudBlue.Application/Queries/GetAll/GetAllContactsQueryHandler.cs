

using AutoMapper;
using CrudBlue.Application.Models.ViewModels;
using CrudBlue.Application.Responses;
using CrudBlue.Domain.Repositories;
using MediatR;

namespace CrudBlue.Application.Queries.GetAll;

public class GetAllContactsQueryHandler : IRequestHandler<GetAllContactsQuery, ServiceResponse<List<ContactViewModel>>>
{
    private readonly IContactRepository _contactRepository;
    private readonly IMapper _mapper;

    public GetAllContactsQueryHandler(IContactRepository contactRepository, IMapper mapper)
    {
        _contactRepository = contactRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<List<ContactViewModel>>> Handle(GetAllContactsQuery request, CancellationToken cancellationToken)
    {
        var contacts = await _contactRepository.GetAllAsync();

        if (contacts == null || !contacts.Any())
        {
            return new ServiceResponse<List<ContactViewModel>>(null, "Nenhum contato encontrado.", false);
        }

        var contactViewModels = _mapper.Map<List<ContactViewModel>>(contacts);

        return new ServiceResponse<List<ContactViewModel>>(contactViewModels, "Contatos obtidos com sucesso.", true);
    }
}
