namespace EnemPrep.Web.Models.Gamificacao;

public class ConquistaViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Icone { get; set; } = string.Empty;
    public int PontosZ { get; set; }
    public bool Desbloqueada { get; set; }
    public DateTime? DataObtencao { get; set; }
}
