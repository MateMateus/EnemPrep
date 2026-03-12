using System.ComponentModel.DataAnnotations;

namespace EnemPrep.Web.Areas.Admin.ViewModels.Assuntos;

public class CriarAssuntoViewModel
{
    [Required]
    public Guid MateriaId { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Nome deve ter entre 2 e 100 caracteres")]
    public string Nome { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    // Para exibir na view como contexto
    public string MateriaNome { get; set; } = string.Empty;
}
