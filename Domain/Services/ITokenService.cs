using Domain.Entity;

namespace Domain.Services;

public interface ITokenService
{
    public string GetToken(User user);
}
