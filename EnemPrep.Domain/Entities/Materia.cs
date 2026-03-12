namespace EnemPrep.Domain.Entities;

public class Materia : Entity
{
    public string Nome { get; private set; }
    public string Descricao { get; private set; }

    private readonly List<Assunto> _assuntos;
    public IReadOnlyCollection<Assunto> Assuntos => _assuntos.AsReadOnly();

    protected Materia() 
    { 
        _assuntos = new List<Assunto>();
        Nome = string.Empty;
        Descricao = string.Empty;
    }

    public Materia(string nome, string descricao) : this()
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome é obrigatório.", nameof(nome));
        
        Nome = nome;
        Descricao = descricao;
    }
}
