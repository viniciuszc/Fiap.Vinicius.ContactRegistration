using Domain.Dtos;
using Domain.Entity;
using Domain.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Service;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("SecretKeyJWT"));

        var tokenProperties = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            }),

            Expires = DateTime.UtcNow.AddHours(1),

            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenProperties);

        return tokenHandler.WriteToken(token);
    }

    public TokenInfo DecodeToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var tokenInfo = new TokenInfo
        {
            Name = jwtToken.Claims.First(claim => claim.Type == "unique_name").Value,
            Email = jwtToken.Claims.First(claim => claim.Type == "email").Value
        };

        return tokenInfo;
    }
}
