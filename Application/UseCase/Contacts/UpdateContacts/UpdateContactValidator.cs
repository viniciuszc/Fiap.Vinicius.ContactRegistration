using Application.UseCase.Contacts.UpdateContact;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase.Contacts.UpdateContacts
{
    public class UpdateContactValidator : AbstractValidator<UpdateContactInput>
    {
        public UpdateContactValidator() 
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("O Id é obrigatório");

            RuleFor(x => x.Name).NotEmpty().WithMessage("O nome é obrigatório");

            RuleFor(x => x.Email).NotEmpty().WithMessage("O Email é obrigatório")
                                 .EmailAddress().WithMessage("O e-mail precisa ser válido");

            RuleFor(x => x.Phone).NotEmpty().WithMessage("O número de Telefone é obrigatório.")
                                 .Matches(@"^\(\d{2,3}\)\s?\d{4,5}-\d{4}$|^\(\d{3}\)\s?9\d{4}-\d{4}$").WithMessage("O número de telefone deve estar no formato (XXX)XXXX-XXXX para Telefones fixos ou (XXX)9XXXX-XXXX para Celulares"); ;
        }
    }
}
