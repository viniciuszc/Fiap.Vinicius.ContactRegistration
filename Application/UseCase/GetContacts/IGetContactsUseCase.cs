using Domain.Entity;

namespace Application.UseCase.GetContacts;

public interface IGetContactsUseCase
{
    Task<IEnumerable<Contact>> GetContactsAsync(CancellationToken cancellationToken);
}
