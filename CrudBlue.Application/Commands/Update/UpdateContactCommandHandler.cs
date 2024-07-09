
using AutoMapper;
using CrudBlue.Application.Models.ViewModels;
using CrudBlue.Application.Responses;
using CrudBlue.Domain.Repositories;
using MediatR;

namespace CrudBlue.Application.Commands.Update;

public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, ServiceResponse<ContactViewModel>>
{
    private readonly IContactRepository _contactRepository;
    private readonly IMapper _mapper;

    public UpdateContactCommandHandler(IContactRepository contactRepository, IMapper mapper)
    {
        _contactRepository = contactRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<ContactViewModel>> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        var existingContact = await _contactRepository.GetByIdAsync(request.Id);

        if (existingContact == null)
        {
            return new ServiceResponse<ContactViewModel>(null, "Contato não encontrado.", false);
        }

        _mapper.Map(request.ContactInput, existingContact);

        var success = await _contactRepository.UpdateContactAsync(existingContact);

        if (success)
        {
            var contactViewModel = _mapper.Map<ContactViewModel>(existingContact);
            return new ServiceResponse<ContactViewModel>(contactViewModel, "Contato atualizado com sucesso.", true);
        }

        return new ServiceResponse<ContactViewModel>(null, "Falha ao atualizar o contato.", false);
    }
}
