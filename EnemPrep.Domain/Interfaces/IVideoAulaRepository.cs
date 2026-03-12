using EnemPrep.Domain.Entities;

namespace EnemPrep.Domain.Interfaces;

public interface IVideoAulaRepository
{
    Task<VideoAula?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<VideoAula>> GetByAssuntoIdAsync(Guid assuntoId, CancellationToken cancellationToken = default);
    Task AddAsync(VideoAula videoAula, CancellationToken cancellationToken = default);
    Task UpdateAsync(VideoAula videoAula, CancellationToken cancellationToken = default);
    Task DeleteAsync(VideoAula videoAula, CancellationToken cancellationToken = default);
}
