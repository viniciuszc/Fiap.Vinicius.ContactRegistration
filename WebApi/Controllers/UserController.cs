using Application.UseCase.Users.CreateUser;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : BaseController
{
    private readonly ICreateUserUseCase _createUserUseCase;
    private readonly ILogger<UserController> _logger;
    private readonly IValidator<CreateUserInput> _validator;

    public UserController(ICreateUserUseCase createUserUseCase, ILogger<UserController> logger, IValidator<CreateUserInput> validator)
    {
        _createUserUseCase = createUserUseCase;
        _logger = logger;
        _validator = validator;
    }

    [HttpPost()]
    public async Task<IActionResult> PostAsync(CreateUserInput input, CancellationToken cancellationToken)
    {
        _logger.LogInformation("UserController - PostAsync - Start");

        var validatorResult = _validator.Validate(input);

        if (!validatorResult.IsValid)
        {
            var errors = validatorResult.Errors.Select(s => s.ErrorMessage);

            _logger.LogWarning("ClientController - PostAsync - Validation Failed: {@Errors}", errors);

            return BadRequest(new { Errors = errors });
        }

        await _createUserUseCase.CreateUserAsync(input, cancellationToken);

        _logger.LogInformation("UserController - PostAsync - E  nd");

        return Ok();
    }
}