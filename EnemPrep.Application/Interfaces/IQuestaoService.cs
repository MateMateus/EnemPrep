using EnemPrep.Application.Common;
using EnemPrep.Application.DTOs.Questoes;

namespace EnemPrep.Application.Interfaces;

public interface IQuestaoService
{
    Task<Result<QuestaoDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<PagedResult<QuestaoDto>>> GetPagedByAssuntoAsync(Guid assuntoId, PagedRequest request, CancellationToken cancellationToken = default);
    Task<Result<QuestaoDto>> CriarAsync(CriarQuestaoRequest request, CancellationToken cancellationToken = default);
    Task<Result<ResultadoQuestaoDto>> ResponderAsync(Guid usuarioId, ResponderQuestaoRequest request, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<TentativaQuestaoDto>>> GetHistoricoAsync(Guid usuarioId, CancellationToken cancellationToken = default);
    Task<Result> DeletarAsync(Guid id, CancellationToken cancellationToken = default);
}
