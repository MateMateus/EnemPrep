namespace EnemPrep.Application.DTOs.Simulados;

public class RespostaSimuladoDto
{
    public Guid QuestaoId { get; set; }
    public Guid? AlternativaId { get; set; }
}

public class SubmeterSimuladoDto
{
    public List<RespostaSimuladoDto> Respostas { get; set; } = new();
}
