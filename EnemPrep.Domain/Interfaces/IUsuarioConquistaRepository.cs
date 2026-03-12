using EnemPrep.Domain.Entities;

namespace EnemPrep.Domain.Interfaces;

public interface IUsuarioConquistaRepository
{
    Task<IEnumerable<UsuarioConquista>> GetByUsuarioIdAsync(Guid usuarioId, CancellationToken cancellationToken = default);
    Task<bool> HasConquistaAsync(Guid usuarioId, Guid conquistaId, CancellationToken cancellationToken = default);
    Task AddAsync(UsuarioConquista usuarioConquista, CancellationToken cancellationToken = default);
}
