using EnemPrep.Domain.Enums;

namespace EnemPrep.Application.DTOs.Questoes;

public record AtualizarQuestaoRequest(
    string Enunciado,
    NivelDificuldade Dificuldade,
    string? Explicacao,
    string? VideoExplicacaoUrl,
    string? ImagemUrl,
    IReadOnlyList<CriarAlternativaRequest> Alternativas,
    Guid? LivroId = null,
    Guid? LivroTemaId = null
);
