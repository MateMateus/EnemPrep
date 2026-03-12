using EnemPrep.Domain.Entities;

namespace EnemPrep.Domain.Interfaces;

public interface IConquistaRepository
{
    Task<IEnumerable<Conquista>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Conquista?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
