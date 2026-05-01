using Microsoft.EntityFrameworkCore;
using EnemPrep.Domain.Entities;
using EnemPrep.Domain.Interfaces;
using EnemPrep.Infrastructure.Persistence;

namespace EnemPrep.Infrastructure.Repositories;

public class AssuntoRepository : IAssuntoRepository
{
    private readonly EnemPrepDbContext _context;

    public AssuntoRepository(EnemPrepDbContext context)
    {
        _context = context;
    }

    public async Task<Assunto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Assuntos
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Assunto>> GetByMateriaIdAsync(Guid materiaId, CancellationToken cancellationToken = default)
    {
        return await _context.Assuntos
            .AsNoTracking()
            .Where(a => a.MateriaId == materiaId)
            .OrderBy(a => a.Nome)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Assunto assunto, CancellationToken cancellationToken = default)
    {
        await _context.Assuntos.AddAsync(assunto, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Assunto assunto, CancellationToken cancellationToken = default)
    {
        _context.Assuntos.Update(assunto);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Assunto assunto, CancellationToken cancellationToken = default)
    {
        _context.Assuntos.Remove(assunto);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteByMateriaIdAsync(Guid materiaId, CancellationToken cancellationToken = default)
    {
        await _context.Assuntos
            .Where(a => a.MateriaId == materiaId)
            .ExecuteDeleteAsync(cancellationToken);
    }
}
