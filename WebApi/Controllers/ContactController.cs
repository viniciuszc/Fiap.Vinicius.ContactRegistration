using Application.UseCase.GetContacts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly IGetContactsUseCase _getContactsUseCase;
    private readonly ILogger<ContactController> _logger;

    public ContactController(IGetContactsUseCase getContactsUseCase, ILogger<ContactController> logger)
    {
        _getContactsUseCase = getContactsUseCase;
        _logger = logger;
    }

    [HttpGet()]
    [Authorize]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("ContactController - GetAsync - Start");

        var result = await _getContactsUseCase.GetContactsAsync(cancellationToken);

        _logger.LogInformation("ContactController - GetAsync - End");

        return Ok(result);
    }
}