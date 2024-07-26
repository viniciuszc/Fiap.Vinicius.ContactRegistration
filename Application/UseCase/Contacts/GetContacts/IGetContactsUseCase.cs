using Domain.Entity;

namespace Application.UseCase.Contacts.GetContacts;

public interface IGetContactsUseCase
{
    Task<IEnumerable<Contact>> GetContactsAsync(string token, CancellationToken cancellationToken);
}
