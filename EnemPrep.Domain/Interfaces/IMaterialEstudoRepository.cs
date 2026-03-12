using EnemPrep.Domain.Entities;

namespace EnemPrep.Domain.Interfaces;

public interface IMaterialEstudoRepository
{
    Task<MaterialEstudo?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MaterialEstudo>> GetByAssuntoIdAsync(Guid assuntoId, CancellationToken cancellationToken = default);
    Task AddAsync(MaterialEstudo material, CancellationToken cancellationToken = default);
    Task UpdateAsync(MaterialEstudo material, CancellationToken cancellationToken = default);
    Task DeleteAsync(MaterialEstudo material, CancellationToken cancellationToken = default);
}
