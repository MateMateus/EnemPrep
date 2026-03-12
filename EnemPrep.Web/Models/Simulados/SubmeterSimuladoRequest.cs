namespace EnemPrep.Web.Models.Simulados;

public class RespostaSimuladoRequest
{
    public Guid QuestaoId { get; set; }
    public Guid? AlternativaId { get; set; }
}

public class SubmeterSimuladoRequest
{
    public List<RespostaSimuladoRequest> Respostas { get; set; } = new();
}
