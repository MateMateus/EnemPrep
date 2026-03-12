namespace EnemPrep.Web.Models.Simulados;

public class TentativaSimuladoResult
{
    public Guid Id { get; set; }
    public Guid SimuladoId { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public int? NotaTotalBruta { get; set; }
}
