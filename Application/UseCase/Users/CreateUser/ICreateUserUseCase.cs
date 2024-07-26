namespace Application.UseCase.Users.CreateUser;

public interface ICreateUserUseCase
{
    Task<bool> CreateUserAsync(CreateUserInput input, CancellationToken cancellationToken);
}
