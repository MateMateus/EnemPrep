using Microsoft.EntityFrameworkCore;
using EnemPrep.Domain.Entities;
using EnemPrep.Domain.Interfaces;
using EnemPrep.Infrastructure.Persistence;

namespace EnemPrep.Infrastructure.Repositories;

public class MateriaRepository : IMateriaRepository
{
    private readonly EnemPrepDbContext _context;

    public MateriaRepository(EnemPrepDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Materia>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Materias
            .AsNoTracking()
            .OrderBy(m => m.Nome)
            .ToListAsync(cancellationToken);
    }

    public async Task<Materia?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Materias
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }

    public async Task<Materia?> GetByIdComAssuntosAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Materias
            .AsNoTracking()
            .Include(m => m.Assuntos)
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }

    public async Task AddAsync(Materia materia, CancellationToken cancellationToken = default)
    {
        await _context.Materias.AddAsync(materia, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Materia materia, CancellationToken cancellationToken = default)
    {
        _context.Materias.Update(materia);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Materia materia, CancellationToken cancellationToken = default)
    {
        _context.Materias.Remove(materia);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
