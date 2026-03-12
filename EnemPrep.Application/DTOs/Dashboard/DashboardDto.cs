namespace EnemPrep.Application.DTOs.Dashboard;

public record DashboardDto(
    int TotalQuestoesRespondidas,
    int TotalAcertos,
    double PercentualAcerto,
    int StreakAtual,
    int MaiorStreak,
    IReadOnlyList<EstatisticasMateriaDto> QuestoesPorMateria
);
