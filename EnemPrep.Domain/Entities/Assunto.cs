namespace EnemPrep.Domain.Entities;

public class Assunto : Entity
{
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    
    public Guid MateriaId { get; private set; }
    public Materia Materia { get; private set; } = null!;

    private readonly List<Questao> _questoes;
    public IReadOnlyCollection<Questao> Questoes => _questoes.AsReadOnly();

    private readonly List<VideoAula> _videoAulas;
    public IReadOnlyCollection<VideoAula> VideoAulas => _videoAulas.AsReadOnly();


    protected Assunto() 
    { 
        _questoes = new List<Questao>();
        _videoAulas = new List<VideoAula>();
        Nome = string.Empty;
        Descricao = string.Empty;
    }

    public Assunto(string nome, string descricao, Guid materiaId) : this()
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome é obrigatório.", nameof(nome));
        if (materiaId == Guid.Empty) throw new ArgumentException("Matéria é obrigatória.", nameof(materiaId));

        Nome = nome;
        Descricao = descricao;
        MateriaId = materiaId;
    }
}
