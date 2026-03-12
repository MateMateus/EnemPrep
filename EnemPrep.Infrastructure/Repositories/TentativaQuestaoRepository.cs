using Microsoft.EntityFrameworkCore;
using EnemPrep.Domain.Entities;
using EnemPrep.Domain.Interfaces;
using EnemPrep.Infrastructure.Persistence;

namespace EnemPrep.Infrastructure.Repositories;

public class TentativaQuestaoRepository : ITentativaQuestaoRepository
{
    private readonly EnemPrepDbContext _context;

    public TentativaQuestaoRepository(EnemPrepDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TentativaQuestao tentativa, CancellationToken cancellationToken = default)
    {
        await _context.TentativasQuestao.AddAsync(tentativa, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TentativaQuestao>> GetByUsuarioIdAsync(Guid usuarioId, CancellationToken cancellationToken = default)
    {
        return await _context.TentativasQuestao
            .AsNoTracking()
            .Where(t => t.UsuarioId == usuarioId)
            .OrderByDescending(t => t.DataCriacao)
            .ToListAsync(cancellationToken);
    }

    public async Task<(int TotalRespondidas, int TotalAcertos)> GetEstatisticasPorAssuntoAsync(
        Guid usuarioId, Guid assuntoId, CancellationToken cancellationToken = default)
    {
        var tentativas = await _context.TentativasQuestao
            .AsNoTracking()
            .Include(t => t.Questao)
            .Where(t => t.UsuarioId == usuarioId && t.Questao.AssuntoId == assuntoId)
            .ToListAsync(cancellationToken);

        return (tentativas.Count, tentativas.Count(t => t.Acertou));
    }

    public async Task<(int TotalRespondidas, int TotalAcertos)> GetEstatisticasGeraisAsync(
        Guid usuarioId, CancellationToken cancellationToken = default)
    {
        var tentativas = await _context.TentativasQuestao
            .AsNoTracking()
            .Where(t => t.UsuarioId == usuarioId)
            .ToListAsync(cancellationToken);

        return (tentativas.Count, tentativas.Count(t => t.Acertou));
    }
}
