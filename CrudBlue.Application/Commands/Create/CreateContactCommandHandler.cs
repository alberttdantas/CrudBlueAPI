
using AutoMapper;
using CrudBlue.Application.Models.ViewModels;
using CrudBlue.Application.Responses;
using CrudBlue.Domain.Entities;
using CrudBlue.Domain.Repositories;
using MediatR;

namespace CrudBlue.Application.Commands.Create;

public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, ServiceResponse<ContactViewModel>>
{
    private readonly IContactRepository _contactRepository;
    private readonly IMapper _mapper;

    public CreateContactCommandHandler(IContactRepository contactRepository, IMapper mapper)
    {
        _contactRepository = contactRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<ContactViewModel>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        var contact = _mapper.Map<Contact>(request.ContactInput);

        var result = await _contactRepository.CreateContactAsync(contact);

        if (result)
        {
            var contactViewModel = _mapper.Map<ContactViewModel>(contact);
            return new ServiceResponse<ContactViewModel>(contactViewModel, "Contato criado com sucesso.", true);
        }

        return new ServiceResponse<ContactViewModel>(null, "Falha ao criar o Contato.", false);
    }
}
