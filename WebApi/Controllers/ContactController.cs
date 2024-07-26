using Application.UseCase.Contacts.CreateContact;
using Application.UseCase.Contacts.DeleteContacts;
using Application.UseCase.Contacts.GetContacts;
using Application.UseCase.Contacts.UpdateContact;
using Application.UseCase.Contacts.UpdateContacts;
using Application.UseCase.GetRegions;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace WebApi.Controllers;

[Route("api/contact")]
[ApiController]
public class ContactController : BaseController
{
    private readonly IGetContactsUseCase _getContactsUseCase;
    private readonly ICreateContactUseCase _createContactUseCase;    
    private readonly IUpdateContactUseCase _updateContactUseCase;
    private readonly IDeleteContactUseCase _deleteContactUseCase;
    private readonly ILogger<ContactController> _logger;
    private readonly IValidator<CreateContactInput> _validatorCreate;
    private readonly IValidator<UpdateContactInput> _validatorUpdate;
    private readonly IGetRegionsUseCase _getRegionsUseCase;

    public ContactController(IGetContactsUseCase getContactsUseCase, 
                                ICreateContactUseCase createContactUseCase, 
                                IUpdateContactUseCase updateContactUseCase, 
                                IDeleteContactUseCase deleteContactUseCase, 
                                IValidator<CreateContactInput> validatorCreate, 
                                IValidator<UpdateContactInput> validatorUpdate, 
                                IGetRegionsUseCase getRegionsUseCase, 
                                ILogger<ContactController> logger)
    {
        _getContactsUseCase   = getContactsUseCase;
        _createContactUseCase = createContactUseCase;        
        _updateContactUseCase = updateContactUseCase;
        _deleteContactUseCase = deleteContactUseCase;
        _logger               = logger;
        _validatorCreate      = validatorCreate;
        _validatorUpdate      = validatorUpdate;
        _getRegionsUseCase    = getRegionsUseCase;
    }

    [HttpGet()]
    //[Authorize]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("ContactController - GetAsync - Start");

        var result = await _getContactsUseCase.GetContactsAsync(GetToken(), cancellationToken);

        _logger.LogInformation("ContactController - GetAsync - End");

        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateContactInput input, CancellationToken cancellationToken)
    {
        try
        {
            var validatorResult = _validatorCreate.Validate(input);

            if (!validatorResult.IsValid)
            {
                var errors = validatorResult.Errors.Select(s => s.ErrorMessage);

                _logger.LogWarning("ContactController - PostAsync - Validation Failed: {Errors}", errors);

                return BadRequest(new { Errors = errors });
            }

            _logger.LogInformation("ContactController - Create - Start");

            await _createContactUseCase.CreateContactAsync(input, GetToken(), cancellationToken);

            _logger.LogInformation("ContactController - Create - End");

            return Ok("Contato criado com sucesso");
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateContactInput input, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("ContactController - Update - Start");

            input.Id = id;
            var validatorResult = _validatorUpdate.Validate(input);

            if (!validatorResult.IsValid)
            {
                var errors = validatorResult.Errors.Select(s => s.ErrorMessage);

                _logger.LogWarning("ContactController - Update - Validation Failed: {@Errors}", errors);

                return BadRequest(new { Errors = errors });
            }

            await _updateContactUseCase.UpdateContactAsync(input, GetToken(), cancellationToken);

            _logger.LogInformation("ContactController - Update - End");

            return Ok("Contato alterado com sucesso");
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    [Authorize]    
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("ContactController - Delete - Start");

            await _deleteContactUseCase.DeleteContactAsync(id, cancellationToken);

            _logger.LogInformation("ContactController - Delete - End");

            return Ok("Contato deletado com sucesso");
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("by-region/{region}")]
    [Authorize]
    public async Task<IActionResult> GetContactsByRegionAsync(string region, CancellationToken cancellationToken)
    {
        _logger.LogInformation("RegionController - GetContactsByRegionAsync - Start");

        var result = await _getRegionsUseCase.GetAllByRegionAsync(region, GetToken(), cancellationToken);

        _logger.LogInformation("RegionController - GetContactsByRegionAsync - End");

        return Ok(result);
    }

    [HttpGet("by-code/{code}")]
    [Authorize]
    public async Task<IActionResult> GetContactsByCodeAsync(string code, CancellationToken cancellationToken)
    {
        _logger.LogInformation("RegionController - GetContactsByCodeAsync - Start");

        var result = await _getRegionsUseCase.GetAllByCodeAsync(code, GetToken(), cancellationToken);

        _logger.LogInformation("RegionController - RegionController - End");

        return Ok(result);
    }
}