using Domain.Entity;

namespace Domain.Repository;

public interface IUserRepository
{
    Task<bool> CreateAsync(User user, CancellationToken cancellationToken);
    Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken);
}
