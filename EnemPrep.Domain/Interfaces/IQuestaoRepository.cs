using EnemPrep.Domain.Entities;

namespace EnemPrep.Domain.Interfaces;

public interface IQuestaoRepository
{
    Task<Questao?> GetByIdComAlternativasAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Questao?> GetByIdTrackingAsync(Guid id, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<Questao> Items, int TotalCount)> GetPagedByAssuntoAsync(Guid assuntoId, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<Questao> Items, int TotalCount)> GetPagedByTemaAsync(Guid temaId, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task AddAsync(Questao questao, CancellationToken cancellationToken = default);
    Task UpdateAsync(Questao questao, CancellationToken cancellationToken = default);
    Task DeleteAsync(Questao questao, CancellationToken cancellationToken = default);
    Task DeleteByAssuntoIdAsync(Guid assuntoId, CancellationToken cancellationToken = default);
    Task DeleteSimuladosQuestoesByQuestaoIdsAsync(IEnumerable<Guid> questaoIds, CancellationToken cancellationToken = default);
    Task DesvincularLivroETemasAsync(Guid livroId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Guid>> GetIdsByAssuntoAsync(Guid assuntoId, CancellationToken cancellationToken = default);
}
