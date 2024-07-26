using Domain.Entity;
using Domain.Repository;
using Domain.Service;
using Microsoft.Extensions.Logging;

namespace Application.UseCase.GetRegions;

public class GetRegionsUseCase : IGetRegionsUseCase
{
    private readonly IDDDRepository _dddRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly ILogger<GetRegionsUseCase> _logger;

    public GetRegionsUseCase(IDDDRepository dddRepository, IUserRepository userRepository, ITokenService tokenService, ILogger<GetRegionsUseCase> logger)
    {
        _dddRepository = dddRepository;
        _userRepository = userRepository;
        _tokenService = tokenService;
        _logger = logger;
    }

    public async Task<IEnumerable<DDD>> GetAllByRegionAsync(string region, string token, CancellationToken cancellationToken)
    {
        var tokenInfo = _tokenService.DecodeToken(token);
        var user = await _userRepository.GetByEmailAsync(tokenInfo.Email, cancellationToken);

        var contacts = await _dddRepository.GetAllByRegionAsync(user.Id, region);
             
        _logger.LogInformation("GetRegionsUseCase - GetAllByRegionAsync - Data: {@data}", contacts);

        return contacts;
    }

    public async Task<IEnumerable<DDD>> GetAllByCodeAsync(string code, string token, CancellationToken cancellationToken)
    {
        var tokenInfo = _tokenService.DecodeToken(token);
        var user = await _userRepository.GetByEmailAsync(tokenInfo.Email, cancellationToken);

        var contacts = await _dddRepository.GetAllByCodeAsync(user.Id, code);

        _logger.LogInformation("GetRegionsUseCase  - GetAllByCodeAsync - Data: {@data}", contacts);

        return contacts;
    }
}
