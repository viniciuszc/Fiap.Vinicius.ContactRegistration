using Domain.Entity;

namespace Domain.Repository;
public interface IDDDRepository
{
    Task<IEnumerable<DDD>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<DDD>> GetAllByRegionAsync(int userId, string region);
    Task<IEnumerable<DDD>> GetAllByCodeAsync(int userId, string code);
    Task<DDD> GetDDDByCodeAsync(string code);
}