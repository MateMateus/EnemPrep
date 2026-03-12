using EnemPrep.Domain.Entities;

namespace EnemPrep.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Usuario?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task AddAsync(Usuario usuario, CancellationToken cancellationToken = default);
    Task UpdateAsync(Usuario usuario, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string email, CancellationToken cancellationToken = default);
    Task<PerfilUsuario?> GetPerfilByTipoAsync(EnemPrep.Domain.Enums.TipoPerfil tipo, CancellationToken cancellationToken = default);
}
