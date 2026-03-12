using Microsoft.EntityFrameworkCore;
using EnemPrep.Domain.Entities;
using EnemPrep.Domain.Interfaces;
using EnemPrep.Infrastructure.Persistence;

namespace EnemPrep.Infrastructure.Repositories;

public class TentativaSimuladoRepository : ITentativaSimuladoRepository
{
    private readonly EnemPrepDbContext _context;

    public TentativaSimuladoRepository(EnemPrepDbContext context)
    {
        _context = context;
    }

    public async Task<TentativaSimulado?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.TentativasSimulado.AsNoTracking().FirstOrDefaultAsync(ts => ts.Id == id, cancellationToken);
    }

    public async Task<TentativaSimulado?> GetByIdWithRespostasAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.TentativasSimulado
            .AsNoTracking()
            .Include(ts => ts.Respostas)
            .FirstOrDefaultAsync(ts => ts.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<TentativaSimulado>> GetByUsuarioIdAsync(Guid usuarioId, CancellationToken cancellationToken = default)
    {
        return await _context.TentativasSimulado
            .Where(ts => ts.UsuarioId == usuarioId)
            .OrderByDescending(ts => ts.DataInicio)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TentativaSimulado tentativa, CancellationToken cancellationToken = default)
    {
        await _context.TentativasSimulado.AddAsync(tentativa, cancellationToken);
    }

    public void Update(TentativaSimulado tentativa)
    {
        foreach (var resp in tentativa.Respostas)
        {
            if (_context.Entry(resp).State == EntityState.Detached)
            {
                _context.Entry(resp).State = EntityState.Added;
            }
        }
        _context.TentativasSimulado.Update(tentativa);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
