namespace EnemPrep.Web.Models.Shared;

public class MateriaViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
}

public class MateriaComAssuntosViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public List<AssuntoViewModel> Assuntos { get; set; } = [];
}
