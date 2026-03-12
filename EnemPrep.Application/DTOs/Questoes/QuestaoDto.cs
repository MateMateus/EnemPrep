using EnemPrep.Domain.Enums;

namespace EnemPrep.Application.DTOs.Questoes;

public record QuestaoDto(
    Guid Id,
    string Enunciado,
    NivelDificuldade Dificuldade,
    Guid AssuntoId,
    string NomeAssunto,
    string? Explicacao,
    IReadOnlyList<AlternativaDto> Alternativas
);
