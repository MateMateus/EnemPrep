namespace EnemPrep.Web.Models.Shared;

public class TentativaQuestaoViewModel
{
    public Guid Id { get; set; }
    public Guid QuestaoId { get; set; }
    public string Enunciado { get; set; } = string.Empty;
    public bool Acertou { get; set; }
    public int TempoGastoSegundos { get; set; }
    public DateTime DataTentativa { get; set; }
}
