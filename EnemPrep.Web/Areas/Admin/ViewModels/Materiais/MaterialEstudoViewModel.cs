namespace EnemPrep.Web.Areas.Admin.ViewModels.Materiais;

public class MaterialEstudoViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string UrlArquivo { get; set; } = string.Empty;
    public Guid AssuntoId { get; set; }
    public string AssuntoNome { get; set; } = string.Empty;
}
