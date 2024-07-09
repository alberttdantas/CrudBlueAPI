
using CrudBlue.Domain.Entities;

namespace CrudBlue.Domain.Repositories;

public interface IContactRepository
{
    Task<IEnumerable<Contact>> GetAllAsync();
    Task<Contact> GetByIdAsync(int? id);
    Task<bool> CreateContactAsync(Contact contact);
    Task<bool> DeleteContactAsync(Contact contact);
    Task<bool> UpdateContactAsync(Contact contact);
    Task<bool> InactivateContactAsync(Contact contact);
}