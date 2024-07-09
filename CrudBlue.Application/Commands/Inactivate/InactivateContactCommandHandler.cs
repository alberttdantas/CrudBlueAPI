
using AutoMapper;
using CrudBlue.Application.Models.ViewModels;
using CrudBlue.Application.Responses;
using CrudBlue.Domain.Entities;
using CrudBlue.Domain.Repositories;
using MediatR;

namespace CrudBlue.Application.Commands.Inactivate;

public class InactivateContactCommandHandler : IRequestHandler<InactivateContactCommand, ServiceResponse<ContactViewModel>>
{
    private readonly IContactRepository _contactRepository;
    private readonly IMapper _mapper;

    public InactivateContactCommandHandler(IContactRepository contactRepository, IMapper mapper)
    {
        _contactRepository = contactRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<ContactViewModel>> Handle(InactivateContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await _contactRepository.GetByIdAsync(request.Id);

        if (contact == null)
        {
            return new ServiceResponse<ContactViewModel>(null, "Contato não encontrado.", false);
        }

        var success = await _contactRepository.InactivateContactAsync(contact);

        if (!success)
        {
            return new ServiceResponse<ContactViewModel>(null, "Erro ao inativar o contato.", false);
        }

        var contactViewModel = _mapper.Map<ContactViewModel>(contact);
        return new ServiceResponse<ContactViewModel>(contactViewModel, "Contato inativado com sucesso.", true);
    }
}
