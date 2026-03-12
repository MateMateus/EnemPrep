using EnemPrep.Domain.Entities;
using EnemPrep.Domain.Interfaces;
using EnemPrep.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EnemPrep.Infrastructure.Repositories;

public class ConquistaRepository : IConquistaRepository
{
    private readonly EnemPrepDbContext _context;

    public ConquistaRepository(EnemPrepDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Conquista>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Conquistas.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Conquista?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Conquistas.FindAsync(new object[] { id }, cancellationToken);
    }
}
