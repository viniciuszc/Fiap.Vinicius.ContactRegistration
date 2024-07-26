using Domain.Entity;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Data.Repository;

public class ContactRepository : IContactRepository
{
    private readonly SqlServerDbContext _context;

    public ContactRepository(SqlServerDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Contact>> GetAllAsync(int userId, CancellationToken cancellationToken)
        => await _context.Contacts.AsNoTracking().Where(it => it.UserId == userId).ToListAsync(cancellationToken);

    public async Task<Contact> GetByEmailAsync(string email, CancellationToken cancellationToken)
        => await _context.Contacts.FirstOrDefaultAsync(contact => contact.Email == email);

    public async Task<bool> CreateAsync(Contact contat, CancellationToken cancellationToken)
    {
        await _context.Contacts.AddAsync(contat, cancellationToken);
        var result = await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> UpdateAsync(Contact contat, CancellationToken cancellationToken)
    {
        _context.Contacts.Update(contat);
        var result = await _context.SaveChangesAsync(cancellationToken);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(int contactId, CancellationToken cancellationToken)
    {
        try
        {
            var contact = _context.Contacts.FirstOrDefault(p => p.Id == contactId);
            if (contact is null) throw new ArgumentException("Contato não encontrado: "+ contactId);

            _context.Contacts.Remove(contact);
            var result = await _context.SaveChangesAsync(cancellationToken);

            return result > 0;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }
}
