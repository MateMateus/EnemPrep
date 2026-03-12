namespace EnemPrep.Web.Areas.Admin.ViewModels.Questoes;

public class QuestaoViewModel
{
    public Guid Id { get; set; }
    public string Enunciado { get; set; } = string.Empty;
    public string Dificuldade { get; set; } = string.Empty;
    public Guid AssuntoId { get; set; }
    public string AssuntoNome { get; set; } = string.Empty;
    public int TotalAlternativas { get; set; }
}
