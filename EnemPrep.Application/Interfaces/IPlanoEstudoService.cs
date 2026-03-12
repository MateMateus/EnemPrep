using EnemPrep.Application.Common;
using EnemPrep.Application.DTOs.PlanoEstudo;

namespace EnemPrep.Application.Interfaces;

public interface IPlanoEstudoService
{
    Task<Result<IReadOnlyList<PlanoEstudoDto>>> GetByUsuarioIdAsync(Guid usuarioId, CancellationToken cancellationToken = default);
    Task<Result<PlanoEstudoDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<PlanoEstudoDto>> CriarAsync(Guid usuarioId, CriarPlanoEstudoRequest request, CancellationToken cancellationToken = default);
    Task<Result> AtualizarStatusItemAsync(Guid planoEstudoItemId, AtualizarStatusPlanoItemRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeletarAsync(Guid id, CancellationToken cancellationToken = default);
}
