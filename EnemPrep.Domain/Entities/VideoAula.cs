namespace EnemPrep.Domain.Entities;

public class VideoAula : Entity
{
    public string Titulo { get; private set; }
    public string UrlVideo { get; private set; }
    public int DuracaoSegundos { get; private set; }

    public Guid AssuntoId { get; private set; }
    public Assunto Assunto { get; private set; } = null!;

    protected VideoAula() 
    { 
        Titulo = string.Empty;
        UrlVideo = string.Empty;
    }

    public VideoAula(string titulo, string urlVideo, int duracaoSegundos, Guid assuntoId)
    {
        if (string.IsNullOrWhiteSpace(titulo)) throw new ArgumentException("Título é obrigatório.", nameof(titulo));
        if (string.IsNullOrWhiteSpace(urlVideo)) throw new ArgumentException("URL do vídeo é obrigatória.", nameof(urlVideo));
        if (assuntoId == Guid.Empty) throw new ArgumentException("Assunto é obrigatório.", nameof(assuntoId));

        Titulo = titulo;
        UrlVideo = urlVideo;
        DuracaoSegundos = duracaoSegundos;
        AssuntoId = assuntoId;
    }
}
