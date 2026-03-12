namespace EnemPrep.Web.Models.Shared;

public class QuestaoViewModel
{
    public Guid Id { get; set; }
    public string Enunciado { get; set; } = string.Empty;
    public string Dificuldade { get; set; } = string.Empty;
    public string? Explicacao { get; set; }
    public Guid AssuntoId { get; set; }
    public string NomeAssunto { get; set; } = string.Empty;
    public List<AlternativaViewModel> Alternativas { get; set; } = [];
}

public class AlternativaViewModel
{
    public Guid Id { get; set; }
    public string Texto { get; set; } = string.Empty;
    public bool IsCorreta { get; set; }
}
