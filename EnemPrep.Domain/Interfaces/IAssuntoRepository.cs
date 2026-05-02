using EnemPrep.Domain.Entities;

namespace EnemPrep.Domain.Interfaces;

public interface IAssuntoRepository
{
    Task<Assunto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Assunto>> GetByMateriaIdAsync(Guid materiaId, CancellationToken cancellationToken = default);
    Task AddAsync(Assunto assunto, CancellationToken cancellationToken = default);
    Task UpdateAsync(Assunto assunto, CancellationToken cancellationToken = default);
    Task DeleteAsync(Assunto assunto, CancellationToken cancellationToken = default);
    Task DeleteByMateriaIdAsync(Guid materiaId, CancellationToken cancellationToken = default);
}
