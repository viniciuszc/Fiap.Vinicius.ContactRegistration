using Domain.Entity;

namespace Application.UseCase.Contacts.CreateContact;

public interface ICreateContactUseCase
{
    Task<bool> CreateContactAsync(CreateContactInput input, string token, CancellationToken cancellationToken);
}
