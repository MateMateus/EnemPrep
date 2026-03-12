using Microsoft.EntityFrameworkCore;
using EnemPrep.Domain.Entities;
using EnemPrep.Domain.Interfaces;
using EnemPrep.Infrastructure.Persistence;

namespace EnemPrep.Infrastructure.Repositories;

public class MaterialEstudoRepository : IMaterialEstudoRepository
{
    private readonly EnemPrepDbContext _context;

    public MaterialEstudoRepository(EnemPrepDbContext context)
    {
        _context = context;
    }

    public async Task<MaterialEstudo?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.MateriaisEstudo
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<MaterialEstudo>> GetByAssuntoIdAsync(Guid assuntoId, CancellationToken cancellationToken = default)
    {
        return await _context.MateriaisEstudo
            .AsNoTracking()
            .Where(m => m.AssuntoId == assuntoId)
            .OrderBy(m => m.Titulo)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(MaterialEstudo material, CancellationToken cancellationToken = default)
    {
        await _context.MateriaisEstudo.AddAsync(material, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(MaterialEstudo material, CancellationToken cancellationToken = default)
    {
        _context.MateriaisEstudo.Update(material);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(MaterialEstudo material, CancellationToken cancellationToken = default)
    {
        _context.MateriaisEstudo.Remove(material);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
