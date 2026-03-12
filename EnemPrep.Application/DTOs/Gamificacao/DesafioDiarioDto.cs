namespace EnemPrep.Application.DTOs.Gamificacao;

public record DesafioDiarioDto(
    Guid Id,
    string Titulo,
    DateTime DataDesafio,
    Guid QuestaoId,
    int XPRecompensa,
    bool Concluido);
