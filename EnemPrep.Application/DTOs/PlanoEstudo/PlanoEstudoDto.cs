using EnemPrep.Domain.Enums;

namespace EnemPrep.Application.DTOs.PlanoEstudo;

public record PlanoEstudoDto(
    Guid Id,
    string Titulo,
    DateTime DataInicio,
    DateTime DataFim,
    IReadOnlyList<PlanoEstudoItemDto> Itens
);
