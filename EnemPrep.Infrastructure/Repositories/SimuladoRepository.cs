using Microsoft.EntityFrameworkCore;
using EnemPrep.Domain.Entities;
using EnemPrep.Domain.Interfaces;
using EnemPrep.Infrastructure.Persistence;

namespace EnemPrep.Infrastructure.Repositories;

public class SimuladoRepository : ISimuladoRepository
{
    private readonly EnemPrepDbContext _context;

    public SimuladoRepository(EnemPrepDbContext context)
    {
        _context = context;
    }

    public async Task<Simulado?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Simulados.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<Simulado?> GetByIdWithQuestoesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Simulados
            .AsNoTracking()
            .Include(s => s.Questoes.OrderBy(q => q.Ordem))
                .ThenInclude(sq => sq.Questao)
                    .ThenInclude(q => q.Assunto)
            .Include(s => s.Questoes)
                .ThenInclude(sq => sq.Questao)
                    .ThenInclude(q => q.Alternativas)
            .AsSplitQuery()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Simulado>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Simulados
            .Include(s => s.Questoes)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Simulado simulado, CancellationToken cancellationToken = default)
    {
        await _context.Simulados.AddAsync(simulado, cancellationToken);
    }

    public void Update(Simulado simulado)
    {
        _context.Simulados.Update(simulado);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoverVinculosPelaQuestaoAsync(Guid questaoId, CancellationToken cancellationToken = default)
    {
        await _context.Set<SimuladoQuestao>()
            .Where(sq => sq.QuestaoId == questaoId)
            .ExecuteDeleteAsync(cancellationToken);
    }
}
