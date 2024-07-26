using Domain.Dtos;
using Domain.Entity;

namespace Domain.Service;

public interface ITokenService
{
    public string GenerateToken(User user);
    public TokenInfo DecodeToken(string token);
}
