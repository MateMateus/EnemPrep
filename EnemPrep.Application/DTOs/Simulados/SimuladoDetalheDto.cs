using EnemPrep.Application.DTOs.Questoes;

namespace EnemPrep.Application.DTOs.Simulados;

public class SimuladoDetalheDto
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public int? AnoReferencia { get; set; }
    public TimeSpan DuracaoMaxima { get; set; }
    public List<QuestaoDto> Questoes { get; set; } = new();
}
