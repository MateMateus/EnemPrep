namespace EnemPrep.Application.DTOs.Questoes;

public record ResponderQuestaoRequest(Guid QuestaoId, Guid AlternativaSelecionadaId, int TempoGastoSegundos);
