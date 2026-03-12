namespace EnemPrep.Web.Models.Shared;

public class DashboardViewModel
{
    public string NomeUsuario { get; set; } = string.Empty;
    public int TotalQuestoesRespondidas { get; set; }
    public int TotalAcertos { get; set; }
    public double PercentualAcerto { get; set; }
    public int StreakAtual { get; set; }
    public int MaiorStreak { get; set; }
    public EnemPrep.Web.Models.Gamificacao.DesafioDiarioViewModel? DesafioDiario { get; set; }
    public List<EstatisticasMateriaViewModel> QuestoesPorMateria { get; set; } = new();

    public int TotalErros => TotalQuestoesRespondidas - TotalAcertos;
}
