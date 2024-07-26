using Application.UseCase.Contacts.CreateContact;
using Application.UseCase.Contacts.GetContacts;
using Application.UseCase.Contacts.UpdateContact;
using Domain.Entity;
using Domain.Repository;
using Domain.Service;
using Microsoft.Extensions.Logging;

namespace Application.UseCase.Contacts.UpdateContacts
{
    public class UpdateContactUseCase : IUpdateContactUseCase
    {
        private readonly IContactRepository _contactRepository;
        private readonly IDDDRepository _DDDRepository;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UpdateContactUseCase> _logger;

        public UpdateContactUseCase(IContactRepository contactRepository, IDDDRepository DDDRepository, ITokenService tokenService, IUserRepository userRepository, ILogger<UpdateContactUseCase> logger)
        {
            _contactRepository = contactRepository;
            _DDDRepository = DDDRepository;
            _tokenService = tokenService;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<bool> UpdateContactAsync(UpdateContactInput input, string token, CancellationToken cancellationToken)
        {
            var tokenInfo = _tokenService.DecodeToken(token);

            var user = await _userRepository.GetByEmailAsync(tokenInfo.Email, cancellationToken);
            var contact = new Contact(user.Id, input.Id, input.Name, input.Phone, input.Email);

            GetDDDId(contact);
            var result = await _contactRepository.UpdateAsync(contact, cancellationToken);

            _logger.LogInformation("UpdateContactsUseCase - UpdateContact - Data: {@data}", contact);
        
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
}
