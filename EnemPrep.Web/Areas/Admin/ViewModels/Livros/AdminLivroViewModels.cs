using System.ComponentModel.DataAnnotations;
using EnemPrep.Web.Enums;

namespace EnemPrep.Web.Areas.Admin.ViewModels.Livros;

public class CriarLivroViewModel
{
    [Required(ErrorMessage = "O título é obrigatório")]
    [StringLength(200, ErrorMessage = "O título deve ter no máximo 200 caracteres")]
    public string Titulo { get; set; } = string.Empty;

    [StringLength(2000, ErrorMessage = "A descrição deve ter no máximo 2000 caracteres")]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "A matéria é obrigatória")]
    [Display(Name = "Matéria")]
    public Guid MateriaId { get; set; }

    [Required(ErrorMessage = "O Tipo de Conteúdo é obrigatório.")]
    public TipoConteudo TipoConteudo { get; set; }

    public IFormFile? CapaArquivo { get; set; }

    public string? UrlCapa { get; set; }
}

public class CriarTemaViewModel
{
    [Required(ErrorMessage = "O nome do tema é obrigatório")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "A página inicial é obrigatória")]
    public int PaginaInicial { get; set; }

    [Required(ErrorMessage = "A página final é obrigatória")]
    public int PaginaFinal { get; set; }
}
