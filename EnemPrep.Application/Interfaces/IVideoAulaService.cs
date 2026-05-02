using EnemPrep.Application.Common;
using EnemPrep.Application.DTOs.VideoAulas;

namespace EnemPrep.Application.Interfaces;

public interface IVideoAulaService
{
    Task<Result<PagedResult<VideoAulaDto>>> GetByAssuntoIdAsync(Guid assuntoId, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<Result<VideoAulaDto>> CriarAsync(CriarVideoAulaRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeletarAsync(Guid id, CancellationToken cancellationToken = default);
}
