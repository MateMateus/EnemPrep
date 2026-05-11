using EnemPrep.Domain.Enums;

namespace EnemPrep.Application.DTOs.Questoes;

public record CriarQuestaoRequest(
    string Enunciado,
    NivelDificuldade Dificuldade,
    Guid AssuntoId,
    string? Explicacao,
    string? VideoExplicacaoUrl,
    string? ImagemUrl,
    IReadOnlyList<CriarAlternativaRequest> Alternativas,
    Guid? LivroId = null,
    Guid? LivroTemaId = null
);
