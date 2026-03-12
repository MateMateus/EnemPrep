namespace EnemPrep.Application.DTOs.Simulados;

public class SimuladoResumoDto
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public int? AnoReferencia { get; set; }
    public TimeSpan DuracaoMaxima { get; set; }
    public int QuantidadeQuestoes { get; set; }
}
