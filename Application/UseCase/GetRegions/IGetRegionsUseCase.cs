using Domain.Entity;

namespace Application.UseCase.GetRegions;

public interface IGetRegionsUseCase
{
    Task<IEnumerable<DDD>> GetAllByRegionAsync(string region, string token, CancellationToken cancellationToken);
    Task<IEnumerable<DDD>> GetAllByCodeAsync(string code, string token, CancellationToken cancellationToken);
}
