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
        // Projecao no SQL — evita carregar objetos de Questao na memoria
        var query = _context.TentativasQuestao
            .AsNoTracking()
            .Where(t => t.UsuarioId == usuarioId && t.Questao.AssuntoId == assuntoId);

        var total = await query.CountAsync(cancellationToken);
        var acertos = await query.CountAsync(t => t.Acertou, cancellationToken);

        return (total, acertos);
    }

    public async Task<(int TotalRespondidas, int TotalAcertos)> GetEstatisticasGeraisAsync(
        Guid usuarioId, CancellationToken cancellationToken = default)
    {
        // Projecao no SQL — nunca carrega todos os registros na memoria
        var query = _context.TentativasQuestao
            .AsNoTracking()
            .Where(t => t.UsuarioId == usuarioId);

        var total = await query.CountAsync(cancellationToken);
        var acertos = await query.CountAsync(t => t.Acertou, cancellationToken);

        return (total, acertos);
    }

    public async Task DeleteByQuestaoIdAsync(Guid questaoId, CancellationToken cancellationToken = default)
    {
        // ExecuteDeleteAsync: DELETE direto no SQL sem carregar entidades na memoria
        await _context.TentativasQuestao
            .Where(t => t.QuestaoId == questaoId)
            .ExecuteDeleteAsync(cancellationToken);
    }
}
