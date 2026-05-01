using Microsoft.EntityFrameworkCore;
using EnemPrep.Domain.Entities;
using EnemPrep.Domain.Interfaces;
using EnemPrep.Infrastructure.Persistence;

namespace EnemPrep.Infrastructure.Repositories;

public class QuestaoRepository : IQuestaoRepository
{
    private readonly EnemPrepDbContext _context;

    public QuestaoRepository(EnemPrepDbContext context)
    {
        _context = context;
    }

    public async Task<Questao?> GetByIdComAlternativasAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Questoes
            .AsNoTracking()
            .Include(q => q.Alternativas)
            .FirstOrDefaultAsync(q => q.Id == id, cancellationToken);
    }

    public async Task<Questao?> GetByIdTrackingAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Questoes
            .Include(q => q.Alternativas)
            .FirstOrDefaultAsync(q => q.Id == id, cancellationToken);
    }

    public async Task<(IReadOnlyList<Questao> Items, int TotalCount)> GetPagedByTemaAsync(
        Guid temaId, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _context.Questoes
            .AsNoTracking()
            .Where(q => q.LivroTemaId == temaId);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Include(q => q.Alternativas)
            .OrderBy(q => q.DataCriacao)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public async Task<(IReadOnlyList<Questao> Items, int TotalCount)> GetPagedByAssuntoAsync(

        Guid assuntoId, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _context.Questoes
            .AsNoTracking()
            .Where(q => q.AssuntoId == assuntoId);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Include(q => q.Alternativas)
            .OrderBy(q => q.DataCriacao)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public async Task AddAsync(Questao questao, CancellationToken cancellationToken = default)
    {
        await _context.Questoes.AddAsync(questao, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Questao questao, CancellationToken cancellationToken = default)
    {
        // Se a questao veio de GetByIdTrackingAsync, o Entry já estará na memória.
        var entry = _context.Entry(questao);
        if (entry.State == EntityState.Detached)
        {
            _context.Questoes.Update(questao);
        }

        // Garante que todas as novas alternativas sejam tratadas como Inserted
        foreach (var alt in questao.Alternativas)
        {
            var altEntry = _context.Entry(alt);
            if (altEntry.State == EntityState.Detached || altEntry.State == EntityState.Modified)
            {
                altEntry.State = EntityState.Added;
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Questao questao, CancellationToken cancellationToken = default)
    {
        _context.Questoes.Remove(questao);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteByAssuntoIdAsync(Guid assuntoId, CancellationToken cancellationToken = default)
    {
        await _context.Questoes
            .Where(q => q.AssuntoId == assuntoId)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task DeleteSimuladosQuestoesByQuestaoIdsAsync(IEnumerable<Guid> questaoIds, CancellationToken cancellationToken = default)
    {
        await _context.Set<SimuladoQuestao>()
            .Where(sq => questaoIds.Contains(sq.QuestaoId))
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task DesvincularLivroETemasAsync(Guid livroId, CancellationToken cancellationToken = default)
    {
        await _context.Questoes
            .Where(q => q.LivroId == livroId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(q => q.LivroId, (Guid?)null)
                .SetProperty(q => q.LivroTemaId, (Guid?)null),
                cancellationToken);
    }

    public async Task<IEnumerable<Guid>> GetIdsByAssuntoAsync(Guid assuntoId, CancellationToken cancellationToken = default)
    {
        return await _context.Questoes
            .Where(q => q.AssuntoId == assuntoId)
            .Select(q => q.Id)
            .ToListAsync(cancellationToken);
    }
}
