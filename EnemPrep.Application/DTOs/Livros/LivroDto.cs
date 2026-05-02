using EnemPrep.Domain.Enums;

namespace EnemPrep.Application.DTOs.Livros;

public record LivroDto(
    Guid Id, 
    string Titulo, 
    string? Descricao, 
    string? UrlCapa, 
    int TotalPaginas, 
    int TotalTemas, 
    DateTime DataCriacao,
    Guid MateriaId,
    string MateriaNome,
    TipoConteudo TipoConteudo);
