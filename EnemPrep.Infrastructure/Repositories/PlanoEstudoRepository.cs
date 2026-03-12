using Microsoft.EntityFrameworkCore;
using EnemPrep.Domain.Entities;
using EnemPrep.Domain.Interfaces;
using EnemPrep.Infrastructure.Persistence;

namespace EnemPrep.Infrastructure.Repositories;

public class PlanoEstudoRepository : IPlanoEstudoRepository
{
    private readonly EnemPrepDbContext _context;

    public PlanoEstudoRepository(EnemPrepDbContext context)
    {
        _context = context;
    }

    public async Task<PlanoEstudo?> GetByIdComItensAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.PlanosEstudo
            .AsNoTracking()
            .Include(p => p.Itens)
                .ThenInclude(i => i.Assunto)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<PlanoEstudo>> GetByUsuarioIdAsync(Guid usuarioId, CancellationToken cancellationToken = default)
    {
        return await _context.PlanosEstudo
            .AsNoTracking()
            .Where(p => p.UsuarioId == usuarioId)
            .OrderByDescending(p => p.DataCriacao)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(PlanoEstudo plano, CancellationToken cancellationToken = default)
    {
        await _context.PlanosEstudo.AddAsync(plano, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(PlanoEstudo plano, CancellationToken cancellationToken = default)
    {
        _context.PlanosEstudo.Update(plano);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PlanoEstudo plano, CancellationToken cancellationToken = default)
    {
        _context.PlanosEstudo.Remove(plano);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
