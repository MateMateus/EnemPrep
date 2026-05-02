using EnemPrep.Domain.Entities;

namespace EnemPrep.Domain.Interfaces;

public interface ITentativaQuestaoRepository
{
    Task AddAsync(TentativaQuestao tentativa, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TentativaQuestao>> GetByUsuarioIdAsync(Guid usuarioId, CancellationToken cancellationToken = default);
    Task<(int TotalRespondidas, int TotalAcertos)> GetEstatisticasPorAssuntoAsync(Guid usuarioId, Guid assuntoId, CancellationToken cancellationToken = default);
    Task<(int TotalRespondidas, int TotalAcertos)> GetEstatisticasGeraisAsync(Guid usuarioId, CancellationToken cancellationToken = default);
    Task DeleteByQuestaoIdAsync(Guid questaoId, CancellationToken cancellationToken = default);
}
