using Microsoft.EntityFrameworkCore;
using EnemPrep.Domain.Entities;
using EnemPrep.Domain.Interfaces;
using EnemPrep.Infrastructure.Persistence;

namespace EnemPrep.Infrastructure.Repositories;

public class StreakUsuarioRepository : IStreakUsuarioRepository
{
    private readonly EnemPrepDbContext _context;

    public StreakUsuarioRepository(EnemPrepDbContext context)
    {
        _context = context;
    }

    public async Task<StreakUsuario?> GetByUsuarioIdAsync(Guid usuarioId, CancellationToken cancellationToken = default)
    {
        return await _context.StreaksUsuario
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.UsuarioId == usuarioId, cancellationToken);
    }

    public async Task AddAsync(StreakUsuario streak, CancellationToken cancellationToken = default)
    {
        await _context.StreaksUsuario.AddAsync(streak, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(StreakUsuario streak, CancellationToken cancellationToken = default)
    {
        _context.StreaksUsuario.Update(streak);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
