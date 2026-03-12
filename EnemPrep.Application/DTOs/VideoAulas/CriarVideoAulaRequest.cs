namespace EnemPrep.Application.DTOs.VideoAulas;

public record CriarVideoAulaRequest(string Titulo, string UrlVideo, int DuracaoSegundos, Guid AssuntoId);
