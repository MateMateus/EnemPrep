using EnemPrep.Domain.Enums;

namespace EnemPrep.Domain.Entities;

public class Questao : Entity
{
    public string Enunciado { get; private set; }
    public NivelDificuldade Dificuldade { get; private set; }
    public string? Explicacao { get; private set; }

    public Guid AssuntoId { get; private set; }
    public Assunto Assunto { get; private set; } = null!;

    private readonly List<Alternativa> _alternativas;
    public IReadOnlyCollection<Alternativa> Alternativas => _alternativas.AsReadOnly();

    private readonly List<TentativaQuestao> _tentativas;
    public IReadOnlyCollection<TentativaQuestao> Tentativas => _tentativas.AsReadOnly();

    protected Questao() 
    { 
        _alternativas = new List<Alternativa>();
        _tentativas = new List<TentativaQuestao>();
        Enunciado = string.Empty;
    }

    public Questao(string enunciado, NivelDificuldade dificuldade, Guid assuntoId, string? explicacao = null) : this()
    {
        if (string.IsNullOrWhiteSpace(enunciado)) throw new ArgumentException("Enunciado é obrigatório.", nameof(enunciado));
        if (assuntoId == Guid.Empty) throw new ArgumentException("Assunto é obrigatório.", nameof(assuntoId));

        Enunciado = enunciado;
        Dificuldade = dificuldade;
        AssuntoId = assuntoId;
        Explicacao = explicacao;
    }

    public void AdicionarAlternativa(Alternativa alternativa)
    {
        if (alternativa == null) throw new ArgumentNullException(nameof(alternativa));
        _alternativas.Add(alternativa);
    }
}
