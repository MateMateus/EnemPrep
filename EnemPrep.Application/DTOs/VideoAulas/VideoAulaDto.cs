namespace EnemPrep.Application.DTOs.VideoAulas;

public record VideoAulaDto(
    Guid Id,
    string Titulo,
    string UrlVideo,
    int DuracaoSegundos,
    Guid AssuntoId,
    string NomeAssunto
);
