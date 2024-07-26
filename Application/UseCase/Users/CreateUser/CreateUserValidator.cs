using FluentValidation;

namespace Application.UseCase.Users.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUserInput>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("O nome é obrigatório.")
                                .NotEmpty().WithMessage("O nome é obrigatório.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("O e-mail é obrigatório.")
                                 .EmailAddress().WithMessage("O e-mail precisa ser válido");

            RuleFor(x => x.Password).NotEmpty().WithMessage("A senha é obrigatória")
                                    .NotEqual(x => x.Name).WithMessage("A senha não pode ser igual ao nome");
        }
    }
}
