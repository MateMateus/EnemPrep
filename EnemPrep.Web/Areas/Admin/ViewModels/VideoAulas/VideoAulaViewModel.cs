namespace EnemPrep.Web.Areas.Admin.ViewModels.VideoAulas;

public class VideoAulaViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string UrlVideo { get; set; } = string.Empty;
    public int DuracaoSegundos { get; set; }
    public Guid AssuntoId { get; set; }
    public string AssuntoNome { get; set; } = string.Empty;
}
