using EnemPrep.Domain.Enums;

namespace EnemPrep.Application.DTOs.Livros;

public record LivroDetalhadoDto(
    Guid Id, 
    string Titulo, 
    string? Descricao, 
    string? UrlCapa, 
    int TotalPaginas, 
    DateTime DataCriacao, 
    List<LivroPaginaDto> Paginas, 
    List<LivroTemaDto> Temas,
    Guid MateriaId,
    string MateriaNome,
    TipoConteudo TipoConteudo);
