using Domain.Entity;
using System;

namespace Domain.Repository;

public interface IContactRepository
{
    Task<IEnumerable<Contact>> GetAllAsync(int userId, CancellationToken cancellationToken);
    Task<Contact> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<bool> CreateAsync(Contact contact, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Contact contact, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int contactId, CancellationToken cancellationToken);
}
