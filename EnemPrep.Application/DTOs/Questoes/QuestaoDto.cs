using EnemPrep.Domain.Enums;

namespace EnemPrep.Application.DTOs.Questoes;

public record QuestaoDto(
    Guid Id,
    string Enunciado,
    NivelDificuldade Dificuldade,
    Guid AssuntoId,
    string NomeAssunto,
    string? Explicacao,
    string? VideoExplicacaoUrl,
    string? ImagemUrl,
    IReadOnlyList<AlternativaDto> Alternativas,
    Guid? LivroId = null,
    Guid? LivroTemaId = null
);

