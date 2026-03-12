namespace EnemPrep.Domain.Entities;

public class TentativaQuestao : Entity
{
    public Guid UsuarioId { get; private set; }
    public Usuario Usuario { get; private set; } = null!;

    public Guid QuestaoId { get; private set; }
    public Questao Questao { get; private set; } = null!;

    public Guid? AlternativaSelecionadaId { get; private set; }
    public Alternativa? AlternativaSelecionada { get; private set; }

    public bool Acertou { get; private set; }
    public int TempoGastoSegundos { get; private set; }

    protected TentativaQuestao() { }

    public TentativaQuestao(Guid usuarioId, Guid questaoId, Guid? alternativaSelecionadaId, bool acertou, int tempoGastoSegundos)
    {
        if (usuarioId == Guid.Empty) throw new ArgumentException("Usuário é obrigatório.", nameof(usuarioId));
        if (questaoId == Guid.Empty) throw new ArgumentException("Questão é obrigatória.", nameof(questaoId));

        UsuarioId = usuarioId;
        QuestaoId = questaoId;
        AlternativaSelecionadaId = alternativaSelecionadaId;
        Acertou = acertou;
        TempoGastoSegundos = tempoGastoSegundos >= 0 ? tempoGastoSegundos : 0;
    }
}
