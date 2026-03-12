using EnemPrep.Domain.Entities;

namespace EnemPrep.Domain.Interfaces;

public interface IMateriaRepository
{
    Task<IReadOnlyList<Materia>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Materia?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Materia?> GetByIdComAssuntosAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Materia materia, CancellationToken cancellationToken = default);
    Task UpdateAsync(Materia materia, CancellationToken cancellationToken = default);
    Task DeleteAsync(Materia materia, CancellationToken cancellationToken = default);
}
