using Domain.Entity;

namespace Domain.Repository;

public interface IContactRepository
{
    Task<IEnumerable<Contact>> GetAllAsync(CancellationToken cancellationToken);
}
