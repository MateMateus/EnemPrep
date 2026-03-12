using System.ComponentModel.DataAnnotations;

namespace EnemPrep.Web.Areas.Admin.ViewModels.Materias;

public class CriarMateriaViewModel
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Nome deve ter entre 2 e 100 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Descrição é obrigatória")]
    [StringLength(500, ErrorMessage = "Descrição deve ter no máximo 500 caracteres")]
    public string Descricao { get; set; } = string.Empty;
}
