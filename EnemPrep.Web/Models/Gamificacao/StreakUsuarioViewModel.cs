namespace EnemPrep.Web.Models.Gamificacao;

public class StreakUsuarioViewModel
{
    public Guid UsuarioId { get; set; }
    public int DiasConsecutivos { get; set; }
    public int MaiorStreak { get; set; }
    public DateTime UltimaAtividade { get; set; }
}
