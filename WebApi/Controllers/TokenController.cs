using Application.UseCase.Users.GetUserToken;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/token")]
[ApiController]
public class TokenController : BaseController
{
    private readonly IGetTokenUseCase _getTokenUseCase;
    private readonly ILogger<ContactController> _logger;

    public TokenController(IGetTokenUseCase getTokenUseCase, ILogger<ContactController> logger)
    {
        _getTokenUseCase = getTokenUseCase;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] GetTokenInput input, CancellationToken cancellationToken)
    {
        _logger.LogInformation("TokenController - PostAsync - Start");

        var token = await _getTokenUseCase.GetTokenAsync(input, cancellationToken);

        if (string.IsNullOrWhiteSpace(token))
            return Unauthorized();

        _logger.LogInformation("TokenController - PostAsync - End");

        return Ok(token);
    }
}