
using CrudBlue.Domain.Entities;
using CrudBlue.Domain.Repositories;
using CrudBlue.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CrudBlue.Infrastructure.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly AgendaDbContext _context;

    public ContactRepository(AgendaDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateContactAsync(Contact contact)
    {
        await _context.Contacts.AddAsync(contact);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteContactAsync(Contact contact)
    {
        _context.Contacts.Remove(contact);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Contact>> GetAllAsync()
    {
        return await _context.Contacts.ToListAsync();
    }

    public async Task<Contact> GetByIdAsync(int? id)
    {
        return await _context.Contacts.FindAsync(id);
    }

    public async Task<bool> InactivateContactAsync(Contact contact)
    {
        contact.Active = false;
        _context.Contacts.Update(contact);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateContactAsync(Contact contact)
    {
        var trackedEntity = _context.Set<Contact>()
            .Local
            .FirstOrDefault(e => e.Id.Equals(contact.Id));

        if (trackedEntity != null)
        {
            _context.Entry(trackedEntity).State = EntityState.Detached;
        }

        _context.Entry(contact).State = EntityState.Modified;

        return await _context.SaveChangesAsync() > 0;
    }
}