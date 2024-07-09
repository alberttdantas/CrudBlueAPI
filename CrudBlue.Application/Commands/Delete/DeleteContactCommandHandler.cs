
using AutoMapper;
using CrudBlue.Application.Models.ViewModels;
using CrudBlue.Application.Responses;
using CrudBlue.Domain.Entities;
using CrudBlue.Domain.Repositories;
using MediatR;

namespace CrudBlue.Application.Commands.Delete;

public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, ServiceResponse<ContactViewModel>>
{
    private readonly IContactRepository _contactRepository;
    private readonly IMapper _mapper;

    public DeleteContactCommandHandler(IContactRepository contactRepository, IMapper mapper)
    {
        _contactRepository = contactRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<ContactViewModel>> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await _contactRepository.GetByIdAsync(request.Id);

        if (contact == null)
        {
            return new ServiceResponse<ContactViewModel>(null, "Contato não encontrado.", false);
        }

        var result = await _contactRepository.DeleteContactAsync(contact);

        if (result)
        {
            var contactViewModel = _mapper.Map<ContactViewModel>(contact);
            return new ServiceResponse<ContactViewModel>(contactViewModel, "Contato deletado com sucesso.", true);
        }

        return new ServiceResponse<ContactViewModel>(null, "Falha ao deletar contato.", false);
    }
}
