namespace EnemPrep.Web.ViewModels.Simulados;

public class TentativaSimuladoResultViewModel
{
    public Guid Id { get; set; }
    public Guid SimuladoId { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public int? NotaTotalBruta { get; set; }
}
