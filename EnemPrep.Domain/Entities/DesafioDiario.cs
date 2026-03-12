namespace EnemPrep.Domain.Entities;

public class DesafioDiario : Entity
{
    public string Titulo { get; private set; }
    public DateTime DataDesafio { get; private set; }
    
    public Guid QuestaoId { get; private set; }
    public Questao Questao { get; private set; } = null!;

    public int XPRecompensa { get; private set; }

    protected DesafioDiario() 
    { 
        Titulo = string.Empty;
    }

    public DesafioDiario(string titulo, DateTime dataDesafio, Guid questaoId, int xpRecompensa)
    {
        if (string.IsNullOrWhiteSpace(titulo)) throw new ArgumentException("Título é obrigatório.", nameof(titulo));
        if (questaoId == Guid.Empty) throw new ArgumentException("Questão é obrigatória.", nameof(questaoId));

        Titulo = titulo;
        DataDesafio = dataDesafio;
        QuestaoId = questaoId;
        XPRecompensa = xpRecompensa > 0 ? xpRecompensa : 0;
    }
}
