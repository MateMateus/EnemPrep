using EnemPrep.Domain.Enums;

namespace EnemPrep.Domain.Entities;

public class Livro : Entity
{
    public string Titulo { get; private set; }
    public string? Descricao { get; private set; }
    public string? UrlCapa { get; private set; }
    
    public Guid MateriaId { get; private set; }
    public Materia Materia { get; private set; } = null!;
    
    public TipoConteudo TipoConteudo { get; private set; }

    private readonly List<LivroPagina> _paginas = new();
    public IReadOnlyCollection<LivroPagina> Paginas => _paginas.AsReadOnly();

    private readonly List<LivroTema> _temas = new();
    public IReadOnlyCollection<LivroTema> Temas => _temas.AsReadOnly();

    public IReadOnlyCollection<Questao> Questoes { get; private set; } = new List<Questao>();

    protected Livro() { Titulo = string.Empty; } 

    public Livro(string titulo, string? descricao, string? urlCapa, Guid materiaId, TipoConteudo tipoConteudo) : this()
    {
        Titulo = titulo;
        Descricao = descricao;
        UrlCapa = urlCapa;
        MateriaId = materiaId;
        TipoConteudo = tipoConteudo;
    }

    public void AtualizarDetalhes(string titulo, string? descricao, string? urlCapa, Guid materiaId, TipoConteudo tipoConteudo)
    {
        Titulo = titulo;
        Descricao = descricao;
        UrlCapa = urlCapa;
        MateriaId = materiaId;
        TipoConteudo = tipoConteudo;
    }
}

