using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
public class BaseController : Controller
{
    protected string GetToken()
    {
        //return string.Empty; //TODO
        
        var authorizationHeader = Request.Headers["Authorization"].ToString();
        return authorizationHeader["Bearer ".Length..].Trim();
    }
}