namespace EnemPrep.Application.DTOs.Questoes;

public record TentativaQuestaoDto(
    Guid Id,
    Guid QuestaoId,
    string Enunciado,
    bool Acertou,
    int TempoGastoSegundos,
    DateTime DataTentativa
);
