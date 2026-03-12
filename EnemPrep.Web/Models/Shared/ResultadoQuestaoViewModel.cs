namespace EnemPrep.Web.Models.Shared;

public class ResultadoQuestaoViewModel
{
    public bool Acertou { get; set; }
    public Guid AlternativaCorretaId { get; set; }
    public string Explicacao { get; set; } = string.Empty;
}
