namespace EnemPrep.Application.DTOs.Questoes;

public record ResultadoQuestaoDto(bool Acertou, Guid AlternativaCorretaId, string? Explicacao);
