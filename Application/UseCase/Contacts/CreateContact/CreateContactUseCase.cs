using Domain.Entity;
using Domain.Repository;
using Domain.Service;
using Microsoft.Extensions.Logging;

namespace Application.UseCase.Contacts.CreateContact;

public class CreateContactUseCase : ICreateContactUseCase
{
    private readonly IContactRepository _contactRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IDDDRepository _DDDRepository;
    private readonly ILogger<CreateContactUseCase> _logger;

    public CreateContactUseCase(
        IContactRepository contactRepository,
        IUserRepository userRepository,
        ITokenService tokenService,
        IDDDRepository DDDRepository,
        ILogger<CreateContactUseCase> logger)
    {
        _contactRepository = contactRepository;
        _userRepository = userRepository;
        _tokenService = tokenService;
        _DDDRepository = DDDRepository;
        _logger = logger;
    }

    public async Task<bool> CreateContactAsync(CreateContactInput input, string token, CancellationToken cancellationToken)
    {        
        var tokenInfo = _tokenService.DecodeToken(token);
        var user = await _userRepository.GetByEmailAsync(tokenInfo.Email, cancellationToken);

        if (await _contactRepository.GetByEmailAsync(input.Email, cancellationToken) is not null)
            throw new InvalidOperationException("Email already in use");

        var newContact = new Contact(user.Id, input.Name, input.Phone, input.Email);
        GetDDDId(newContact);

        var result = await _contactRepository.CreateAsync(newContact, cancellationToken);

        _logger.LogInformation("CreateContactsUseCase - CreateContactsAsync - Data: {@data}", input);

        return result;
    }

    private void GetDDDId(Contact contact)
    {
        var code = contact.Phone.Substring(1, 3);
        var ddd = _DDDRepository.GetDDDByCodeAsync(code).Result;

        if (ddd == null) throw new ArgumentException("DDD não encontrado: " + code);

        contact.DDDId = ddd.Id;
    }
}
