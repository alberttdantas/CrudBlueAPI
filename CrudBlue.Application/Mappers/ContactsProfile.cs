using AutoMapper;
using CrudBlue.Application.Models.InputModels;
using CrudBlue.Application.Models.ViewModels;
using CrudBlue.Domain.Entities;

public class ContactsProfile : Profile
{
    public ContactsProfile()
    {
        CreateMap<Contact, ContactViewModel>();

        CreateMap<ContactInputModel, Contact>();
    }
}