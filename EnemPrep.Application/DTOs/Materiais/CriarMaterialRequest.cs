using EnemPrep.Domain.Enums;

namespace EnemPrep.Application.DTOs.Materiais;

public record CriarMaterialRequest(string Titulo, TipoMaterialEstudo Tipo, string UrlArquivo, Guid AssuntoId);
