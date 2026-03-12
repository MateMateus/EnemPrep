using EnemPrep.Application.Common;
using EnemPrep.Application.DTOs.Simulados;

namespace EnemPrep.Application.Interfaces;

public interface ISimuladoService
{
    Task<Result<IEnumerable<SimuladoResumoDto>>> GetSimuladosDisponiveisAsync(CancellationToken cancellationToken = default);
    Task<Result<SimuladoDetalheDto>> GetSimuladoByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<TentativaSimuladoResultDto>> IniciarSimuladoAsync(Guid usuarioId, IniciarSimuladoDto request, CancellationToken cancellationToken = default);
    Task<Result<TentativaSimuladoResultDto>> SubmeterSimuladoAsync(Guid usuarioId, Guid tentativaId, SubmeterSimuladoDto request, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<TentativaSimuladoResultDto>>> GetHistoricoTentativasAsync(Guid usuarioId, CancellationToken cancellationToken = default);
}
