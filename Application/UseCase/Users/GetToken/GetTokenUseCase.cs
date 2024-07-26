using Domain.Repository;
using Domain.Service;
using Microsoft.Extensions.Logging;

namespace Application.UseCase.Users.GetUserToken;

public class GetTokenUseCase : IGetTokenUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly ITokenService _tokenService;
    private readonly ILogger<GetTokenUseCase> _logger;

    public GetTokenUseCase(
        IUserRepository userRepository, 
        IPasswordService passwordService, 
        ITokenService tokenService, 
        ILogger<GetTokenUseCase> logger)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _tokenService = tokenService;
        _logger = logger;
    }

    public async Task<string> GetTokenAsync(GetTokenInput input, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(input.Email, cancellationToken);

        if (user is null)
            return string.Empty;

        var isPasswordValid = _passwordService.Verify(input.Password, user.Password);

        if (!isPasswordValid)
            return string.Empty;

        var token = _tokenService.GenerateToken(user);
        
        _logger.LogInformation("GetTokenUseCase - GetTokenAsync - Token: {@data}", token);

        return token;
    }
}