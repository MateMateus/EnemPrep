using EnemPrep.Application.Common;
using EnemPrep.Application.DTOs.Gamificacao;

namespace EnemPrep.Application.Interfaces;

public interface IGamificacaoService
{
    Task<Result<StreakUsuarioDto>> GetStreakAsync(Guid usuarioId, CancellationToken cancellationToken = default);
    Task<Result<DesafioDiarioDto?>> GetDesafioDiarioAsync(Guid usuarioId, DateTime data, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<ConquistaDto>>> GetConquistasDoUsuarioAsync(Guid usuarioId, CancellationToken cancellationToken = default);
    Task RegistrarAtividadeDiariaAsync(Guid usuarioId, CancellationToken cancellationToken = default);
    Task VerificarEAtualizarDesafioDiarioAsync(Guid usuarioId, Guid questaoRespondidaId, bool acertou, CancellationToken cancellationToken = default);
    Task VerificarConquistasPorQuestoesAsync(Guid usuarioId, CancellationToken cancellationToken = default);
}
