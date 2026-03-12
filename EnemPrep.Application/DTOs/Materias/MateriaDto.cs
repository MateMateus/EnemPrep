namespace EnemPrep.Application.DTOs.Materias;

public record MateriaDto(
    Guid Id,
    string Nome,
    string Descricao,
    int QuantidadeAssuntos
);
