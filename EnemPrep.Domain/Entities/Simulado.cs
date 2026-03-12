namespace EnemPrep.Domain.Entities;

public class Simulado : Entity
{
    public string Titulo { get; private set; }
    public int? AnoReferencia { get; private set; }
    public TimeSpan DuracaoMaxima { get; private set; }

    private readonly List<SimuladoQuestao> _questoes;
    public IReadOnlyCollection<SimuladoQuestao> Questoes => _questoes.AsReadOnly();

    private readonly List<TentativaSimulado> _tentativas;
    public IReadOnlyCollection<TentativaSimulado> Tentativas => _tentativas.AsReadOnly();

    protected Simulado() 
    { 
        _questoes = new List<SimuladoQuestao>();
        _tentativas = new List<TentativaSimulado>();
        Titulo = string.Empty;
    }

    public Simulado(string titulo, int? anoReferencia, TimeSpan duracaoMaxima) : this()
    {
        if (string.IsNullOrWhiteSpace(titulo)) throw new ArgumentException("Título é obrigatório.", nameof(titulo));
        if (duracaoMaxima <= TimeSpan.Zero) throw new ArgumentException("Duração deve ser maior que zero.", nameof(duracaoMaxima));

        Titulo = titulo;
        AnoReferencia = anoReferencia;
        DuracaoMaxima = duracaoMaxima;
    }
}
