using Application.UseCase.Users.CreateUser;
using Domain.Entity;
using Domain.Repository;
using Domain.Service;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.Application.UseCases.Users;

public class CreateUserUseCaseTest
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPasswordService> _passwordServiceMock;
    private readonly Mock<ILogger<CreateUserUseCase>> _loggerMock;
    private readonly ICreateUserUseCase _useCase;

    public CreateUserUseCaseTest()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _passwordServiceMock = new Mock<IPasswordService>();
        _loggerMock = new Mock<ILogger<CreateUserUseCase>>();
        _useCase = new CreateUserUseCase(_userRepositoryMock.Object, _passwordServiceMock.Object, _loggerMock.Object);
    }

    [Fact(DisplayName = "Should create user successfully")]
    public async Task ShouldCreateUserSuccessfully()
    {
        var input = new CreateUserInput
        {
            Name = "Name",
            Email = "email@email.com.br",
            Password = "Password"
        };

        _userRepositoryMock.Setup(x => x.GetByEmailAsync(input.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(null as User);

        _userRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var output = await _useCase.CreateUserAsync(input, It.IsAny<CancellationToken>());

        output.Should().BeTrue();
    }

    [Fact(DisplayName = "Should throw if invalid credentials")]
    public async Task ShouldThrowIfContactAlreadyRegistered()
    {
        var input = new CreateUserInput
        {
            Name = "Name",
            Email = "email@email.com.br",
            Password = "Password"
        };

        var userResponse = new User("Teste", input.Email, input.Password);

        _userRepositoryMock.Setup(x => x.GetByEmailAsync(input.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(userResponse);

        Func<Task> act = async () => await _useCase.CreateUserAsync(input, It.IsAny<CancellationToken>());

        await act.Should().ThrowAsync<InvalidOperationException>();
    }
}