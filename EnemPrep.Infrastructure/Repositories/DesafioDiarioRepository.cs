using EnemPrep.Domain.Entities;
using EnemPrep.Domain.Interfaces;
using EnemPrep.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EnemPrep.Infrastructure.Repositories;

public class DesafioDiarioRepository : IDesafioDiarioRepository
{
    private readonly EnemPrepDbContext _context;

    public DesafioDiarioRepository(EnemPrepDbContext context)
    {
        _context = context;
    }

    public async Task<DesafioDiario?> GetDesafioDoDiaAsync(DateTime data, CancellationToken cancellationToken = default)
    {
        return await _context.DesafiosDiarios
            .AsNoTracking()
            .Include(d => d.Questao)
            .FirstOrDefaultAsync(d => d.DataDesafio.Date == data.Date, cancellationToken);
    }

    public async Task<DesafioDiario?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.DesafiosDiarios
            .AsNoTracking()
            .Include(d => d.Questao)
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
    }

    public async Task AddAsync(DesafioDiario desafio, CancellationToken cancellationToken = default)
    {
        await _context.DesafiosDiarios.AddAsync(desafio, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
