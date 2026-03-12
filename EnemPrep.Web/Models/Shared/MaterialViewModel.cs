namespace EnemPrep.Web.Models.Shared;

public class MaterialViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public string UrlArquivo { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public Guid AssuntoId { get; set; }
}
