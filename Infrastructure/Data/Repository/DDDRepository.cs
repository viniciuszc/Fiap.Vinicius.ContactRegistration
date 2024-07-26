using Domain.Entity;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository;

public class DDDRepository : IDDDRepository
{
    private readonly SqlServerDbContext _context;

    public DDDRepository(SqlServerDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DDD>> GetAllAsync(CancellationToken cancellationToken)
        => await _context.DDDs
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<DDD>> GetAllByRegionAsync(int userId, string region)
        => await _context.DDDs
            .Include(x => x.Contacts.Where(it => it.UserId == userId))
            .Where(x => x.Region == region)
            .ToListAsync();

    public async Task<IEnumerable<DDD>> GetAllByCodeAsync(int userId, string code)
        => await _context.DDDs
            .Include(x => x.Contacts.Where(it => it.UserId == userId))
            .Where(x => x.Code == code)            
            .ToListAsync();

    public async Task<DDD?> GetDDDByCodeAsync(string code) 
        => await _context.DDDs
                    .Where(x => x.Code == code)
                    .FirstOrDefaultAsync();
}
