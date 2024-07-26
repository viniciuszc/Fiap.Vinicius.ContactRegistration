using Application.UseCase.Contacts.GetContacts;
using Domain.Repository;
using Microsoft.Extensions.Logging;

namespace Application.UseCase.Contacts.DeleteContacts
{
    public class DeleteContactUseCase : IDeleteContactUseCase
    {
        private readonly IContactRepository _contactRepository;
        private readonly ILogger<DeleteContactUseCase> _logger;

        public DeleteContactUseCase(IContactRepository contactRepository, ILogger<DeleteContactUseCase> logger)
        {
            _contactRepository = contactRepository;
            _logger = logger;
        }

        public async Task<bool> DeleteContactAsync(int contactId, CancellationToken cancellation)
        {
            try
            {
                var result = await _contactRepository.DeleteAsync(contactId, cancellation);
                _logger.LogInformation("DeleteContactsUseCase - DeleteContact - Data: {@data}", contactId);

                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
