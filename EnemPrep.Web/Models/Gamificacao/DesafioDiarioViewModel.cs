namespace EnemPrep.Web.Models.Gamificacao;

public class DesafioDiarioViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public DateTime DataDesafio { get; set; }
    public Guid QuestaoId { get; set; }
    public int XPRecompensa { get; set; }
    public bool Concluido { get; set; }
}
