namespace EnemPrep.Domain.Entities;

public class Alternativa : Entity
{
    public string Texto { get; private set; }
    public bool Correta { get; private set; }

    public Guid QuestaoId { get; private set; }
    public Questao Questao { get; private set; } = null!;

    protected Alternativa() 
    { 
        Texto = string.Empty;
    }

    public Alternativa(string texto, bool correta, Guid questaoId)
    {
        if (string.IsNullOrWhiteSpace(texto)) throw new ArgumentException("Texto da alternativa é obrigatório.", nameof(texto));
        if (questaoId == Guid.Empty) throw new ArgumentException("Questão é obrigatória.", nameof(questaoId));

        Texto = texto;
        Correta = correta;
        QuestaoId = questaoId;
    }
}
