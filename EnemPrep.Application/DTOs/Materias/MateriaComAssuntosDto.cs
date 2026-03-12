namespace EnemPrep.Application.DTOs.Materias;

public record MateriaComAssuntosDto(
    Guid Id,
    string Nome,
    string Descricao,
    IReadOnlyList<AssuntoDto> Assuntos
);
