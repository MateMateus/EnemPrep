namespace EnemPrep.Application.DTOs.Dashboard;

public record EstatisticasMateriaDto(
    Guid MateriaId,
    string NomeMateria,
    int TotalRespondidas,
    int TotalAcertos,
    double PercentualAcerto
);
