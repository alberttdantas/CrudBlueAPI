
using AutoMapper;
using CrudBlue.Application.Models.ViewModels;
using CrudBlue.Application.Responses;
using CrudBlue.Domain.Repositories;
using MediatR;

namespace CrudBlue.Application.Queries.GetById;

public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQuery, ServiceResponse<ContactViewModel>>
{
    private readonly IContactRepository _contactRepository;
    private readonly IMapper _mapper;

    public GetContactByIdQueryHandler(IContactRepository contactRepository, IMapper mapper)
    {
        _contactRepository = contactRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<ContactViewModel>> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
    {
        var contact = await _contactRepository.GetByIdAsync(request.Id);

        if (contact == null)
        {
            return new ServiceResponse<ContactViewModel>(null, "Contato não encontrado.", false);
        }

        var contactViewModel = _mapper.Map<ContactViewModel>(contact);

        return new ServiceResponse<ContactViewModel>(contactViewModel, "Contato obtido com sucesso.", true);
    }
}
