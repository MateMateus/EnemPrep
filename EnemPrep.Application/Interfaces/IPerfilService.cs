using EnemPrep.Application.Common;
using EnemPrep.Application.DTOs.Perfil;

namespace EnemPrep.Application.Interfaces;

public interface IPerfilService
{
    Task<Result<PerfilUsuarioDto>> GetPerfilAsync(Guid usuarioId, CancellationToken cancellationToken = default);
    Task<Result> AtualizarNomeAsync(Guid usuarioId, string novoNome, CancellationToken cancellationToken = default);
    Task<Result> AtualizarEmailAsync(Guid usuarioId, string novoEmail, CancellationToken cancellationToken = default);
    Task<Result> AtualizarSenhaAsync(Guid usuarioId, string senhaAtual, string novaSenha, CancellationToken cancellationToken = default);
}
