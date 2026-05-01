using Microsoft.EntityFrameworkCore;
using EnemPrep.Domain.Entities;
using EnemPrep.Domain.Interfaces;
using EnemPrep.Infrastructure.Persistence;

namespace EnemPrep.Infrastructure.Repositories;

public class VideoAulaRepository : IVideoAulaRepository
{
    private readonly EnemPrepDbContext _context;

    public VideoAulaRepository(EnemPrepDbContext context)
    {
        _context = context;
    }

    public async Task<VideoAula?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.VideoAulas
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<VideoAula>> GetByAssuntoIdAsync(Guid assuntoId, CancellationToken cancellationToken = default)
    {
        return await _context.VideoAulas
            .AsNoTracking()
            .Where(v => v.AssuntoId == assuntoId)
            .OrderBy(v => v.Titulo)
            .ToListAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<VideoAula> Items, int TotalCount)> GetPagedByAssuntoIdAsync(Guid assuntoId, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _context.VideoAulas
            .AsNoTracking()
            .Where(v => v.AssuntoId == assuntoId);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(v => v.Titulo)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public async Task AddAsync(VideoAula videoAula, CancellationToken cancellationToken = default)
    {
        await _context.VideoAulas.AddAsync(videoAula, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(VideoAula videoAula, CancellationToken cancellationToken = default)
    {
        _context.VideoAulas.Update(videoAula);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(VideoAula videoAula, CancellationToken cancellationToken = default)
    {
        _context.VideoAulas.Remove(videoAula);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteByAssuntoIdAsync(Guid assuntoId, CancellationToken cancellationToken = default)
    {
        await _context.VideoAulas
            .Where(v => v.AssuntoId == assuntoId)
            .ExecuteDeleteAsync(cancellationToken);
    }
}
