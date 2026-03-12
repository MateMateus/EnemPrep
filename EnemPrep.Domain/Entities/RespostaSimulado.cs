namespace EnemPrep.Domain.Entities;

public class RespostaSimulado : Entity
{
    public Guid TentativaSimuladoId { get; private set; }
    public TentativaSimulado TentativaSimulado { get; private set; } = null!;

    public Guid QuestaoId { get; private set; }
    public Questao Questao { get; private set; } = null!;

    public Guid? AlternativaId { get; private set; } 
    public Alternativa? Alternativa { get; private set; } 

    public bool Correta { get; private set; }

    protected RespostaSimulado() { }

    public RespostaSimulado(Guid tentativaSimuladoId, Guid questaoId, Guid? alternativaId, bool correta)
    {
        if (tentativaSimuladoId == Guid.Empty) throw new ArgumentException("Tentativa inválida.", nameof(tentativaSimuladoId));
        if (questaoId == Guid.Empty) throw new ArgumentException("Questão inválida.", nameof(questaoId));

        TentativaSimuladoId = tentativaSimuladoId;
        QuestaoId = questaoId;
        AlternativaId = alternativaId;
        Correta = correta;
    }
}
