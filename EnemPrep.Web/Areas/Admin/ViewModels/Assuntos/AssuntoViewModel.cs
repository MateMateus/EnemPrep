namespace EnemPrep.Web.Areas.Admin.ViewModels.Assuntos;

public class AssuntoViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public Guid MateriaId { get; set; }
    public string MateriaNome { get; set; } = string.Empty;
}
