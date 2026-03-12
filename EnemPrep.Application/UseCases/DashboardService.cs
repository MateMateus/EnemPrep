using EnemPrep.Application.Common;
using EnemPrep.Application.DTOs.Dashboard;
using EnemPrep.Application.Interfaces;
using EnemPrep.Domain.Interfaces;

namespace EnemPrep.Application.UseCases;

public class DashboardService : IDashboardService
{
    private readonly ITentativaQuestaoRepository _tentativaRepository;
    private readonly IStreakUsuarioRepository _streakRepository;
    private readonly IMateriaRepository _materiaRepository;

    public DashboardService(
        ITentativaQuestaoRepository tentativaRepository,
        IStreakUsuarioRepository streakRepository,
        IMateriaRepository materiaRepository)
    {
        _tentativaRepository = tentativaRepository;
        _streakRepository = streakRepository;
        _materiaRepository = materiaRepository;
    }

    public async Task<Result<DashboardDto>> GetDashboardAsync(Guid usuarioId, CancellationToken cancellationToken)
    {
        var (totalRespondidas, totalAcertos) = await _tentativaRepository.GetEstatisticasGeraisAsync(usuarioId, cancellationToken);

        var streak = await _streakRepository.GetByUsuarioIdAsync(usuarioId, cancellationToken);
        var streakAtual = streak?.DiasConsecutivos ?? 0;
        var maiorStreak = streak?.MaiorStreak ?? 0;

        var percentual = totalRespondidas > 0
            ? Math.Round((double)totalAcertos / totalRespondidas * 100, 1)
            : 0;

        var materias = await _materiaRepository.GetAllAsync(cancellationToken);
        var estatisticasPorMateria = new List<EstatisticasMateriaDto>();

        foreach (var materia in materias)
        {
            var totalPorMateria = 0;
            var acertosPorMateria = 0;

            foreach (var assunto in materia.Assuntos)
            {
                var (respondidas, acertos) = await _tentativaRepository.GetEstatisticasPorAssuntoAsync(
                    usuarioId, assunto.Id, cancellationToken);
                totalPorMateria += respondidas;
                acertosPorMateria += acertos;
            }

            if (totalPorMateria > 0)
            {
                estatisticasPorMateria.Add(new EstatisticasMateriaDto(
                    materia.Id,
                    materia.Nome,
                    totalPorMateria,
                    acertosPorMateria,
                    Math.Round((double)acertosPorMateria / totalPorMateria * 100, 1)));
            }
        }

        var dashboard = new DashboardDto(
            totalRespondidas,
            totalAcertos,
            percentual,
            streakAtual,
            maiorStreak,
            estatisticasPorMateria);

        return Result<DashboardDto>.Ok(dashboard);
    }
}
