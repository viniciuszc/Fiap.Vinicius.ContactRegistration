using Domain.Entity;
using Domain.Repository;
using Domain.Service;
using Microsoft.Extensions.Logging;

namespace Application.UseCase.Users.CreateUser;

public class CreateUserUseCase : ICreateUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly ILogger<CreateUserUseCase> _logger;

    public CreateUserUseCase(IUserRepository userRepository, IPasswordService passwordService, ILogger<CreateUserUseCase> logger)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _logger = logger;
    }

    public async Task<bool> CreateUserAsync(CreateUserInput input, CancellationToken cancellationToken)
    {
        var newUser = new User(input.Name, input.Email, _passwordService.Hash(input.Password));

        if (await _userRepository.GetByEmailAsync(newUser.Email, cancellationToken) is not null)
            throw new InvalidOperationException("Email already in use");

        var success = await _userRepository.CreateAsync(newUser, cancellationToken);

        _logger.LogInformation("CreateUserUseCase - CreateUserAsync - Data: {@data}", new { newUser.Name, newUser.Email });

        return success;
    }
}
