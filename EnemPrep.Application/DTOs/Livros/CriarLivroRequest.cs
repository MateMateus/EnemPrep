using System.ComponentModel.DataAnnotations;

using EnemPrep.Domain.Enums;

namespace EnemPrep.Application.DTOs.Livros;

public record CriarLivroRequest(
    [Required][StringLength(200)] string Titulo,
    [StringLength(2000)] string? Descricao,
    [StringLength(500)] string? UrlCapa,
    [Required] Guid MateriaId,
    [Required] TipoConteudo TipoConteudo);
