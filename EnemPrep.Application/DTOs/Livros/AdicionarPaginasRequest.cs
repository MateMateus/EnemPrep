using System.ComponentModel.DataAnnotations;

namespace EnemPrep.Application.DTOs.Livros;

public record AdicionarPaginasRequest(
    [Required] IList<string> UrlsImagens);
