using EnemPrep.Web.Models.Shared;

namespace EnemPrep.Web.ViewModels.Simulados;

public class SimuladoDetalheViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public int? AnoReferencia { get; set; }
    public TimeSpan DuracaoMaxima { get; set; }
    public List<QuestaoViewModel> Questoes { get; set; } = new();
}
