namespace EnemPrep.Domain.Entities;

public class PlanoEstudo : Entity
{
    public string Titulo { get; private set; }
    public DateTime DataInicio { get; private set; }
    public DateTime DataFim { get; private set; }

    public Guid UsuarioId { get; private set; }
    public Usuario Usuario { get; private set; } = null!;

    private readonly List<PlanoEstudoItem> _itens;
    public IReadOnlyCollection<PlanoEstudoItem> Itens => _itens.AsReadOnly();

    protected PlanoEstudo() 
    { 
        _itens = new List<PlanoEstudoItem>();
        Titulo = string.Empty;
    }

    public PlanoEstudo(string titulo, DateTime dataInicio, DateTime dataFim, Guid usuarioId) : this()
    {
        if (string.IsNullOrWhiteSpace(titulo)) throw new ArgumentException("Título é obrigatório.", nameof(titulo));
        if (usuarioId == Guid.Empty) throw new ArgumentException("Usuário é obrigatório.", nameof(usuarioId));
        if (dataInicio > dataFim) throw new ArgumentException("Data de início não pode ser posteriror à data fim.");

        Titulo = titulo;
        DataInicio = dataInicio;
        DataFim = dataFim;
        UsuarioId = usuarioId;
    }

    public void AdicionarItem(Guid assuntoId, DateTime dataPrevista)
    {
        var item = new PlanoEstudoItem(Id, assuntoId, dataPrevista);
        _itens.Add(item);
    }
}
