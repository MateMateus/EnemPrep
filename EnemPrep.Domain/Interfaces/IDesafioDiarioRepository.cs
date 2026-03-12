using EnemPrep.Domain.Entities;

namespace EnemPrep.Domain.Interfaces;

public interface IDesafioDiarioRepository
{
    Task<DesafioDiario?> GetDesafioDoDiaAsync(DateTime data, CancellationToken cancellationToken = default);
    Task<DesafioDiario?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(DesafioDiario desafio, CancellationToken cancellationToken = default);
}
