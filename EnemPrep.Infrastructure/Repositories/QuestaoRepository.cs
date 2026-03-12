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
        _context.Questoes.Update(questao);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Questao questao, CancellationToken cancellationToken = default)
    {
        _context.Questoes.Remove(questao);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
