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

    public string VideoId
    {
        get
        {
            var url = UrlVideo ?? "";
            if (url.Contains("youtu.be/"))
            {
                return url.Split("youtu.be/").Last().Split("?").First().Split("&").First();
            }
            if (url.Contains("v="))
            {
                return url.Split("v=").Last().Split("&").First();
            }
            if (url.Contains("embed/"))
            {
                return url.Split("embed/").Last().Split("?").First();
            }
            return string.Empty;
        }
    }

    public string ThumbnailUrl => string.IsNullOrEmpty(VideoId) ? "" : $"https://img.youtube.com/vi/{VideoId}/maxresdefault.jpg";
}
