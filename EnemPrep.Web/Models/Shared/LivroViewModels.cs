using EnemPrep.Web.Enums;

namespace EnemPrep.Web.Models.Shared;

public class LivroListViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public string? UrlCapa { get; set; }
    public int TotalPaginas { get; set; }
    public int TotalTemas { get; set; }
    public DateTime DataCriacao { get; set; }

    public Guid MateriaId { get; set; }
    public string MateriaNome { get; set; } = string.Empty;
    public TipoConteudo TipoConteudo { get; set; }
}

public class LivroDetalhesViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public string? UrlCapa { get; set; }
    public int TotalPaginas { get; set; }
    public DateTime DataCriacao { get; set; }

    public Guid MateriaId { get; set; }
    public string MateriaNome { get; set; } = string.Empty;
    public TipoConteudo TipoConteudo { get; set; }

    public List<LivroPaginaViewModel> Paginas { get; set; } = [];
    public List<LivroTemaViewModel> Temas { get; set; } = [];
}

public class LivroPaginaViewModel
{
    public Guid Id { get; set; }
    public int NumeroProprio { get; set; }
    public string UrlImagem { get; set; } = string.Empty;
}

public class LivroTemaViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int PaginaInicial { get; set; }
    public int PaginaFinal { get; set; }
}
