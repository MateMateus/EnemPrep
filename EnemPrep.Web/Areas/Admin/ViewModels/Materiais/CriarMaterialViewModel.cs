using System.ComponentModel.DataAnnotations;

namespace EnemPrep.Web.Areas.Admin.ViewModels.Materiais;

public class CriarMaterialViewModel
{
    [Required]
    public Guid AssuntoId { get; set; }

    public string AssuntoNome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Título é obrigatório")]
    [StringLength(200, MinimumLength = 3)]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Tipo é obrigatório")]
    public string Tipo { get; set; } = "PDF"; // PDF | Resumo | MapaMental | LinkExterno

    [Required(ErrorMessage = "URL do arquivo é obrigatória")]
    [Url(ErrorMessage = "Informe uma URL válida")]
    public string UrlArquivo { get; set; } = string.Empty;
}
