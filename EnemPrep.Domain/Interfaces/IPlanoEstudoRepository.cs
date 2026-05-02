using EnemPrep.Domain.Entities;

namespace EnemPrep.Domain.Interfaces;

public interface IPlanoEstudoRepository
{
    Task<PlanoEstudo?> GetByIdComItensAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PlanoEstudo>> GetByUsuarioIdAsync(Guid usuarioId, CancellationToken cancellationToken = default);
    Task AddAsync(PlanoEstudo plano, CancellationToken cancellationToken = default);
    Task UpdateAsync(PlanoEstudo plano, CancellationToken cancellationToken = default);
    Task DeleteAsync(PlanoEstudo plano, CancellationToken cancellationToken = default);

    Task<PlanoEstudoItem?> GetItemByIdAsync(Guid itemId, CancellationToken cancellationToken = default);
    Task UpdateItemAsync(PlanoEstudoItem item, CancellationToken cancellationToken = default);
}
