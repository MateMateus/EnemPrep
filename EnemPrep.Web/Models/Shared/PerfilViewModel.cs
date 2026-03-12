namespace EnemPrep.Web.Models.Shared;

public class PerfilViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }
    public int QuestoesRespondidas { get; set; }
    public int QuestoesCorretas { get; set; }
    public int StreakDias { get; set; }
}
