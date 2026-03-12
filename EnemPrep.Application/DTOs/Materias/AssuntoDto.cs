namespace EnemPrep.Application.DTOs.Materias;

public record AssuntoDto(
    Guid Id,
    string Nome,
    string Descricao,
    Guid MateriaId
);
