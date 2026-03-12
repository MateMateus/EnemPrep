using EnemPrep.Domain.Entities;
using EnemPrep.Domain.Interfaces;
using EnemPrep.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EnemPrep.Infrastructure.Repositories;

public class UsuarioConquistaRepository : IUsuarioConquistaRepository
{
    private readonly EnemPrepDbContext _context;

    public UsuarioConquistaRepository(EnemPrepDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UsuarioConquista>> GetByUsuarioIdAsync(Guid usuarioId, CancellationToken cancellationToken = default)
    {
        return await _context.UsuarioConquistas
            .AsNoTracking()
            .Include(uc => uc.Conquista)
            .Where(uc => uc.UsuarioId == usuarioId)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> HasConquistaAsync(Guid usuarioId, Guid conquistaId, CancellationToken cancellationToken = default)
    {
        return await _context.UsuarioConquistas
            .AnyAsync(uc => uc.UsuarioId == usuarioId && uc.ConquistaId == conquistaId, cancellationToken);
    }

    public async Task AddAsync(UsuarioConquista usuarioConquista, CancellationToken cancellationToken = default)
    {
        await _context.UsuarioConquistas.AddAsync(usuarioConquista, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
