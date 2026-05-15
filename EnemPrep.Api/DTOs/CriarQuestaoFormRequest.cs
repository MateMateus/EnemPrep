using EnemPrep.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace EnemPrep.Api.DTOs;

/// <summary>
/// Form-bindable DTO for creating questions via multipart/form-data.
/// ASP.NET Core cannot bind positional records with IReadOnlyList from form data,
/// so this class with public setters handles the binding correctly.
/// </summary>
public class CriarQuestaoFormRequest
{
    public string Enunciado { get; set; } = string.Empty;
    public NivelDificuldade Dificuldade { get; set; }
    public Guid AssuntoId { get; set; }
    public string? Explicacao { get; set; }
    public string? VideoExplicacaoUrl { get; set; }
    public string? ImagemUrl { get; set; }
    public IFormFile? ImagemArquivo { get; set; }
    public string AlternativasJson { get; set; } = string.Empty;
    public Guid? LivroId { get; set; }
    public Guid? LivroTemaId { get; set; }
}

/// <summary>
/// Form-bindable DTO for updating questions via multipart/form-data.
/// </summary>
public class AtualizarQuestaoFormRequest
{
    public string Enunciado { get; set; } = string.Empty;
    public NivelDificuldade Dificuldade { get; set; }
    public string? Explicacao { get; set; }
    public string? VideoExplicacaoUrl { get; set; }
    public string? ImagemUrl { get; set; }
    public IFormFile? ImagemArquivo { get; set; }
    public string AlternativasJson { get; set; } = string.Empty;
    public Guid? LivroId { get; set; }
    public Guid? LivroTemaId { get; set; }
}

/// <summary>
/// Form-bindable alternative item (class with setters instead of positional record).
/// </summary>
public class AlternativaFormItem
{
    public string Texto { get; set; } = string.Empty;
    public bool Correta { get; set; }
}

