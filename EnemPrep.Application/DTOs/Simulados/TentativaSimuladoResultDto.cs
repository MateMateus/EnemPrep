namespace EnemPrep.Application.DTOs.Simulados;

public class TentativaSimuladoResultDto
{
    public Guid Id { get; set; }
    public Guid SimuladoId { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public int? NotaTotalBruta { get; set; }
}
