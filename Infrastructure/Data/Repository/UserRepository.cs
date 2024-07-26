using Domain.Entity;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository;

public class UserRepository : IUserRepository
{
    private readonly SqlServerDbContext _context;

    public UserRepository(SqlServerDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateAsync(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        var result = await _context.SaveChangesAsync(cancellationToken);

        return result > 0;
    }

    public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
        => await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
}