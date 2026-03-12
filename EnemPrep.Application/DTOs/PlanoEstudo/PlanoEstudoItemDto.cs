using EnemPrep.Domain.Enums;

namespace EnemPrep.Application.DTOs.PlanoEstudo;

public record PlanoEstudoItemDto(
    Guid Id,
    Guid AssuntoId,
    string NomeAssunto,
    DateTime DataPrevista,
    StatusPlanoItem Status
);
