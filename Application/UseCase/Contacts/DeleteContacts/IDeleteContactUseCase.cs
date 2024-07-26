using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase.Contacts.DeleteContacts
{
    public interface IDeleteContactUseCase
    {
        Task<bool> DeleteContactAsync(int contactId, CancellationToken cancellation);
    }
}
