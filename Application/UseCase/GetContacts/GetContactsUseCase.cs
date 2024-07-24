using Domain.Entity;
using Domain.Repository;
using Microsoft.Extensions.Logging;

namespace Application.UseCase.GetContacts;

public class GetContactsUseCase : IGetContactsUseCase
{
    private readonly IContactRepository _contactRepository;
    private readonly ILogger<GetContactsUseCase> _logger;

    public GetContactsUseCase(IContactRepository contactRepository, ILogger<GetContactsUseCase> logger)
    {
        _contactRepository = contactRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Contact>> GetContactsAsync(CancellationToken cancellationToken)
    {
        var contacts = await _contactRepository.GetAllAsync(cancellationToken);

        _logger.LogInformation("GetContactsUseCase - GetContactsAsync - Data: {@data}", contacts);

        return contacts;
    }
}
