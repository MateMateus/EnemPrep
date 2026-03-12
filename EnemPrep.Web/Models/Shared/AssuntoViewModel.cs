namespace EnemPrep.Web.Models.Shared;

public class AssuntoViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public Guid MateriaId { get; set; }
    public string MateriaNome { get; set; } = string.Empty;
}
