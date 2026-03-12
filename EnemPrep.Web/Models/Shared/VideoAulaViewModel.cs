namespace EnemPrep.Web.Models.Shared;

public class VideoAulaViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string UrlVideo { get; set; } = string.Empty;
    public int DuracaoSegundos { get; set; }
    public Guid AssuntoId { get; set; }

    public string DuracaoFormatada =>
        DuracaoSegundos >= 3600
            ? TimeSpan.FromSeconds(DuracaoSegundos).ToString(@"h\:mm\:ss")
            : TimeSpan.FromSeconds(DuracaoSegundos).ToString(@"m\:ss");
}
