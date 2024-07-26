using Domain.Entity;
using Domain.Repository;
using Domain.Service;
using Microsoft.Extensions.Logging;

namespace Application.UseCase.Contacts.GetContacts;

public class GetContactsUseCase : IGetContactsUseCase
{
    private readonly IContactRepository _contactRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly ILogger<GetContactsUseCase> _logger;

    public GetContactsUseCase(
       IContactRepository contactRepository,
       IUserRepository userRepository,
       ITokenService tokenService,
       ILogger<GetContactsUseCase> logger)
    {
        _contactRepository = contactRepository;
        _userRepository = userRepository;
        _tokenService = tokenService;
        _logger = logger;
    }

    public async Task<IEnumerable<Contact>> GetContactsAsync(string token, CancellationToken cancellationToken)
    {
        //var tokenInfo = _tokenService.DecodeToken(token);

        //var user = await _userRepository.GetByEmailAsync(tokenInfo.Email, cancellationToken);

        //var contacts = await _contactRepository.GetAllAsync(user.Id, cancellationToken);

        var contacts = await _contactRepository.GetAllAsync(1, cancellationToken);

        _logger.LogInformation("GetContactsUseCase - GetContactsAsync - Data: {@data}", contacts);

        return contacts;
    }
}
