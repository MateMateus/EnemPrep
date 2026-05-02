using EnemPrep.Domain.Entities;

namespace EnemPrep.Domain.Interfaces;

public interface ISimuladoRepository
{
    Task<Simulado?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Simulado?> GetByIdWithQuestoesAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Simulado>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Simulado simulado, CancellationToken cancellationToken = default);
    void Update(Simulado simulado);
    Task RemoverVinculosPelaQuestaoAsync(Guid questaoId, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
