using EnemPrep.Domain.Entities;

namespace EnemPrep.Domain.Interfaces;

public interface ITentativaSimuladoRepository
{
    Task<TentativaSimulado?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<TentativaSimulado?> GetByIdWithRespostasAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TentativaSimulado>> GetByUsuarioIdAsync(Guid usuarioId, CancellationToken cancellationToken = default);
    Task AddAsync(TentativaSimulado tentativa, CancellationToken cancellationToken = default);
    void Update(TentativaSimulado tentativa);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
