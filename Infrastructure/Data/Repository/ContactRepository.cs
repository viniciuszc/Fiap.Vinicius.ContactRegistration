using Domain.Entity;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository;

public class ContactRepository : IContactRepository
{
    private readonly SqlServerDbContext _context;

    public ContactRepository(SqlServerDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Contact>> GetAllAsync(CancellationToken cancellationToken)
        => await _context.Contacts.ToListAsync(cancellationToken);
}
