namespace Application.UseCase.Users.GetUserToken;

public interface IGetTokenUseCase
{
    Task<string> GetTokenAsync(GetTokenInput input, CancellationToken cancellationToken);
}
