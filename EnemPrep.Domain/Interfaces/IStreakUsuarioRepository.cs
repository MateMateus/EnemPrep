using EnemPrep.Domain.Entities;

namespace EnemPrep.Domain.Interfaces;

public interface IStreakUsuarioRepository
{
    Task<StreakUsuario?> GetByUsuarioIdAsync(Guid usuarioId, CancellationToken cancellationToken = default);
    Task AddAsync(StreakUsuario streak, CancellationToken cancellationToken = default);
    Task UpdateAsync(StreakUsuario streak, CancellationToken cancellationToken = default);
}
