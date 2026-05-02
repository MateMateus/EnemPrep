using EnemPrep.Domain.Enums;

namespace EnemPrep.Domain.Entities;

public class Questao : Entity
{
    public string Enunciado { get; private set; }
    public NivelDificuldade Dificuldade { get; private set; }
    public string? Explicacao { get; private set; }
    public string? VideoExplicacaoUrl { get; private set; }

    public Guid AssuntoId { get; private set; }
    public Assunto Assunto { get; private set; } = null!;

    public Guid? LivroId { get; private set; }
    public Livro? Livro { get; private set; }

    public Guid? LivroTemaId { get; private set; }
    public LivroTema? LivroTema { get; private set; }

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

    public Questao(string enunciado, NivelDificuldade dificuldade, Guid assuntoId, string? explicacao = null, string? videoExplicacaoUrl = null) : this()
    {
        if (string.IsNullOrWhiteSpace(enunciado)) throw new ArgumentException("Enunciado é obrigatório.", nameof(enunciado));
        if (assuntoId == Guid.Empty) throw new ArgumentException("Assunto é obrigatório.", nameof(assuntoId));

        Enunciado = enunciado;
        Dificuldade = dificuldade;
        AssuntoId = assuntoId;
        Explicacao = explicacao;
        VideoExplicacaoUrl = videoExplicacaoUrl;
    }

    public void AdicionarAlternativa(Alternativa alternativa)
    {
        if (alternativa == null) throw new ArgumentNullException(nameof(alternativa));
        _alternativas.Add(alternativa);
    }

    public void VincularAoLivro(Guid? livroId, Guid? livroTemaId)
    {
        LivroId = livroId;
        LivroTemaId = livroTemaId;
    }

    public void Atualizar(string enunciado, NivelDificuldade dificuldade, string? explicacao, string? videoExplicacaoUrl)
    {
        if (string.IsNullOrWhiteSpace(enunciado)) throw new ArgumentException("Enunciado é obrigatório.", nameof(enunciado));

        Enunciado = enunciado;
        Dificuldade = dificuldade;
        Explicacao = explicacao;
        VideoExplicacaoUrl = videoExplicacaoUrl;
    }

    public void SincronizarAlternativas(IEnumerable<(string Texto, bool Correta)> novasAlternativasArr)
    {
        var novas = novasAlternativasArr.ToList();
        
        for (int i = 0; i < Math.Min(_alternativas.Count, novas.Count); i++)
        {
            _alternativas[i].Atualizar(novas[i].Texto, novas[i].Correta);
        }

        for (int i = _alternativas.Count; i < novas.Count; i++)
        {
            _alternativas.Add(new Alternativa(novas[i].Texto, novas[i].Correta, Id));
        }

        if (_alternativas.Count > novas.Count)
        {
            int toRemove = _alternativas.Count - novas.Count;
            for (int i = 0; i < toRemove; i++)
            {
               _alternativas.RemoveAt(_alternativas.Count - 1);
            }
        }
    }
}
