using System.ComponentModel.DataAnnotations;

namespace EnemPrep.Web.Areas.Admin.ViewModels.VideoAulas;

public class CriarVideoAulaViewModel
{
    [Required]
    public Guid AssuntoId { get; set; }

    public string? AssuntoNome { get; set; }

    [Required(ErrorMessage = "Título é obrigatório")]
    [StringLength(200, MinimumLength = 3)]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "URL do vídeo é obrigatória")]
    [Url(ErrorMessage = "Informe uma URL válida")]
    public string UrlVideo { get; set; } = string.Empty;

    [Range(1, 86400, ErrorMessage = "Duração em segundos deve estar entre 1 e 86400")]
    public int DuracaoSegundos { get; set; }
}
