using System.ComponentModel.DataAnnotations;

namespace EnemPrep.Application.DTOs.Livros;

public record CriarTemaRequest(
    [Required][StringLength(200)] string Nome,
    [Required] int PaginaInicial,
    [Required] int PaginaFinal);
