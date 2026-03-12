namespace EnemPrep.Domain.Entities;

public class SimuladoQuestao : Entity
{
    public Guid SimuladoId { get; private set; }
    public Simulado Simulado { get; private set; } = null!;

    public Guid QuestaoId { get; private set; }
    public Questao Questao { get; private set; } = null!;

    public int Ordem { get; private set; }

    protected SimuladoQuestao() { }

    public SimuladoQuestao(Guid simuladoId, Guid questaoId, int ordem)
    {
        if (simuladoId == Guid.Empty) throw new ArgumentException("Simulado inválido.", nameof(simuladoId));
        if (questaoId == Guid.Empty) throw new ArgumentException("Questão inválida.", nameof(questaoId));

        SimuladoId = simuladoId;
        QuestaoId = questaoId;
        Ordem = ordem;
    }
}
