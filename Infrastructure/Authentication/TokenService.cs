using Domain.Entity;
using Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetToken(User user)
    {
        // Variável responsável por gerar o token
        var tokenHandler = new JwtSecurityTokenHandler();

        // Recupera a chave que criamos no nosso appSettings e convert para um array de bytes
        var chaveCriptografia = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("SecretKeyJWT"));

        // O Descriptor é responsável por definir todas as propriedades que o nosso token terá quando descriptografarmos
        var tokenPropriedades = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, (user.PermissaoSistema - 1).ToString()),
                    // É possível adicionar Claims personalizadas, conforme o exemplo abaixo
                    new Claim("ClaimPersonalizada_1", "Nossa claim 1"),
                    new Claim("ClaimPersonalizada_2", "Nossa claim 2")
            }),

            // Tempo de expiração do token
            Expires = DateTime.UtcNow.AddHours(8),

            // Adiciona a nossa chave de criptografia
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(chaveCriptografia),
                SecurityAlgorithms.HmacSha256Signature)
        };

        // Cria o nosso token e devolve pro método solicitante
        var token = tokenHandler.CreateToken(tokenPropriedades);
        return tokenHandler.WriteToken(token);

    }
}
