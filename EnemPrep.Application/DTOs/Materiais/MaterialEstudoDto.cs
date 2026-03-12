using EnemPrep.Domain.Enums;

namespace EnemPrep.Application.DTOs.Materiais;

public record MaterialEstudoDto(
    Guid Id,
    string Titulo,
    TipoMaterialEstudo Tipo,
    string UrlArquivo,
    Guid AssuntoId,
    string NomeAssunto
);
