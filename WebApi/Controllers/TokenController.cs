using Domain.Entity;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public TokenController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost]
    public IActionResult Post([FromBody] User user)
    {
        var token = _tokenService.GetToken(user);

        if (string.IsNullOrWhiteSpace(token))
            return Unauthorized();

        return Ok(token);
    }
}